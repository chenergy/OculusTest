using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class Galaxy : MonoBehaviour
{
	public class Ellipse {
		public float sin;
		public float cos;
		public float velocity;
		public float eratio;
	}

	public class StarCluster {
		public List <Vector3> vectorPoints;
		public List <StarData> starData;
		public int num;
	}

	public class StarData {
		//public Transform star;
		public ParticleSystem.Particle star;
		public float angle;
		public int radius;
	}

	// For trig lookup tables
	private const float PI2 = Mathf.PI * 2.0f; 
	private const int TABLE_SIZE = 1024 * 128;
	private const float TABLE_SIZE_D = (float)TABLE_SIZE;
	private const float FACTOR = TABLE_SIZE_D / PI2;

	private static float[] _CosineFloatTable;
	private static float[] _SineFloatTable;

	// Galaxy variables
	public int Rmax = 4000; //-- radius of galaxy
	public float Rker = 500f; //-- radius of kernel

	public float eratio = .8f;
	public float etwist = 7.5f;
	public float speed = 200f; 

	// Particle system
	public ParticleSystem particleSystem;
	int total = 0;
	int numStars = 1000;
	ParticleSystem.Particle[] particles;

	List <StarCluster> starClusters = new List<StarCluster> ();
	List <Ellipse> ellipses = new List <Ellipse> ();


	void Start () {
		InitializeTrigonometricTables ();

		for (int r = 0; r < Rmax; r++) {
			float b = etwist * r / Rmax;
			Ellipse newEllipse = new Ellipse ();
			//newEllipse.sin = r * Mathf.Sin (b);
			//newEllipse.cos = r * Mathf.Cos (b);
			newEllipse.sin = r * this.Sin (b);
			newEllipse.cos = r * this.Cos (b);
			newEllipse.velocity = speed / (r + 1);
			newEllipse.eratio = eratio;
			this.ellipses.Add (newEllipse);
		}
			
		StartCoroutine ("WaitForParticleSystem");
	}


	IEnumerator WaitForParticleSystem (){
		while (this.particleSystem.particleCount < 1) {
			yield return new WaitForEndOfFrame ();
		}

		this.particles = new ParticleSystem.Particle[this.particleSystem.particleCount];
		this.numStars = this.particleSystem.GetParticles (this.particles);

		this.InitStarCluster (this.numStars);
	}


	private static void InitializeTrigonometricTables(){
		_CosineFloatTable = new float[TABLE_SIZE];
		_SineFloatTable = new float[TABLE_SIZE];

		for (int i = 0; i < TABLE_SIZE; i++){
			float Angle = (1.0f * i / TABLE_SIZE_D) * PI2;
			_SineFloatTable[i] = Mathf.Sin(Angle);
			_CosineFloatTable[i] = Mathf.Cos(Angle);
		}
	}


	void InitStarCluster (int num) {
		this.total += num;
		StarCluster cluster = new StarCluster ();
		cluster.num = num;
		cluster.vectorPoints = new List <Vector3> ();
		cluster.starData = new List <StarData> ();

		for (int i = 0; i < num; i++) {
			StarData data = new StarData ();
			float angle = PI2 * Random.value;
			int radius = Random.Range (0, Rmax);
			data.angle = angle;
			data.radius = radius;
			cluster.starData.Add (data);

			float thickness = 30f;
			int sign = (Random.value > 0.5f) ? -1 : 1;

			if (radius < Rker)
				thickness += Mathf.Sqrt (Rker * Rker - radius * radius);

			Vector3 target = Vector3.ClampMagnitude (new Vector3 (0, 0, thickness * Random.value - thickness) / 
				Mathf.Clamp(radius, 0.1f, Rmax) * sign, Rmax * 0.1f);
			cluster.vectorPoints.Add (target);

			data.star = this.particles [i];
		}

		this.starClusters.Add (cluster);
	}


	private float Sin (float value){
		value %= PI2;  // In case that the angle is larger than 2pi
		if (value < 0) value += PI2; // in case that the angle is negative
		int index = (int)(value * FACTOR); //from radians to index and casted in to an int
		return _SineFloatTable[index]; // get the value from the table
	}


	private float Cos (float value){
		value %= PI2;  // In case that the angle is larger than 2pi
		if (value < 0) value += PI2; // in case that the angle is negative
		int index = (int)(value * FACTOR); //from radians to index and casted in to an int
		return _CosineFloatTable[index]; // get the value from the table
	}


	void Update (){
		foreach (StarCluster cluster in this.starClusters) {
			for (int i = 0; i < cluster.starData.Count; i++){
				StarData star = cluster.starData[i];
				Vector3 vec = cluster.vectorPoints [i];
				Ellipse ellipse = this.ellipses [star.radius];
				star.angle += Time.deltaTime * ellipse.velocity;

				//vec.x = Mathf.Sin (star.angle);
				//vec.y = ellipse.eratio * Mathf.Cos (star.angle);
				vec.x = this.Sin (star.angle);
				vec.y = ellipse.eratio * this.Cos (star.angle);

				cluster.vectorPoints [i] = new Vector3 (
					ellipse.sin * vec.x + ellipse.cos * vec.y, 
					ellipse.cos * vec.x - ellipse.sin * vec.y, 
					vec.z);

				this.particles [i].position = cluster.vectorPoints [i];
			}
		}

		if (this.starClusters.Count > 1) {
			foreach (StarCluster cluster in this.starClusters) {
				int i1 = Random.Range (0, cluster.num);

				for (int j = 1; j < this.starClusters.Count; j++) {
					int i2 = Random.Range (0, starClusters [j].num);
					Vector3 vec2 = this.starClusters [j].vectorPoints [i2 - 1];

					this.starClusters [j - 1].starData [i1] = this.starClusters [j].starData [i2];
					this.starClusters [j - 1].vectorPoints [i1 - 1] = vec2;
					i1 = i2;
				}
			}
		}

		this.particleSystem.SetParticles (this.particles, this.numStars);
	}
}

