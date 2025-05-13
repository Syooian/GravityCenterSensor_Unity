using UnityEngine;

public class Oscillator : MonoBehaviour
{
    [Range(0, 1000)]
    public double frequency = 400.0f;

    private double increment;
    private double phase;
    private readonly double sampleRate = 48000.0f;

    [Range(0, 4)]
    public float gain = 0.5f;

    [Range(0, 32)]
    public int c = 10;

    [Range(-32, 32)]
    public int ind = 1;

    [Range(-256, 256)]
    public float z = 5f;

    [Range(0, 2)]
    public float a = 0.75f;

    [Range(-0, 4)]
    public float b = 7f;

    [Range(0, 10)]
    public byte choice = 0;

    public Color c1 = Color.yellow;
    public Color c2 = Color.red;
    private readonly int lengthOfLineRenderer = 16384;

    void Start()
    {
        ind = 1;
        c = 10;
        gain = 0.5f;
        LineRenderer lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.widthMultiplier = 0.02f;
        lineRenderer.positionCount = lengthOfLineRenderer;
        lineRenderer.useWorldSpace = false;

        // A simple 2 color gradient with a fixed alpha of 1.0f.
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(c1, 0.0f), new GradientColorKey(c2, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha, 0.0f), new GradientAlphaKey(alpha, 1.0f) }
        );
        lineRenderer.colorGradient = gradient;
    }

    void Update()
    {
        LineRenderer lineRenderer = GetComponent<LineRenderer>();
        AudioSource source = gameObject.GetComponent<AudioSource>();
        float[] samples = new float[16384];
        source.GetOutputData(samples, 0);


        for (int i = 0; i < lengthOfLineRenderer; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(i * 0.5f, samples[i], 0.0f));
        }
    }

    private void OnAudioFilterRead(float[] data, int channels)
    {

        increment = frequency * 2.0f * Mathf.PI / sampleRate;
        for (int i = 0; i < data.Length - 1; i += channels)
        {
            switch (choice)
            {
                case 0: //sawtooth
                    for (int j = ind; j < c; j++)
                    {
                        data[i] += gain * (Mathf.Sin((float)(phase * j)) / j);
                    }
                    break;
                case 1: //square
                    for (int j = ind; j < c; j++)
                    {
                        data[i] += gain * (Mathf.Sin((float)(phase * (2 * j - 1))) / (2 * j - 1));
                    }
                    break;
                case 2: //triangle
                    for (int j = ind; j < c; j++)
                    {
                        data[i] += gain * Mathf.Sin((float)(phase * (2 * j - 1) + Mathf.PI * ((j + 1) % 2))) / Mathf.Pow(2 * j - 1, 2);
                    }
                    break;
                case 3: //harmonics
                    for (int j = ind; j < c; j++)
                    {
                        data[i] += gain * Mathf.Cos((float)(j * phase + z));
                    }
                    break;
                case 4: //Weierstrass (Holder continuity)
                    for (int j = 0; j < c; j++)
                    {
                        data[i] += gain * (Mathf.Pow(b, -j * a) * Mathf.Cos((float)(Mathf.Pow(b, j) * Mathf.PI * phase))); //cool organ sound: C = 32, A = 1.06, B = 2
                    }
                    break;
                default: //sine
                    data[i] = gain * Mathf.Sin((float)phase);
                    break;
            }

            phase += increment;

            if (channels == 2)
            {
                data[i + 1] = data[i];
            }
            // ring mod: Mathf.Sin((double)((Mathf.Pow((double)(phase * j), 3)) * frequency / sampleRate)); data[i] += (double)(gain * (Mathf.Sin((double)((Mathf.Pow((double)(phase * j), 3))))));
            // sawtooth: data[i] += (double)(gain * (Mathf.Sin((double)(phase * j)) / j));
            // square: data[i] += (double)(gain * (Mathf.Sin((double)(phase * (2 * j - 1))) / (2* j - 1)));
            // triangle: data[i] += (double)(gain * (Mathf.Sin((double)(phase * (2 * j - 1) + Mathf.PI * ((j + 1) % 2)))) / Mathf.Pow((2 * j - 1), 2));
            // accidental bitcrushed sine: data[i] += (double)(gain * (Mathf.Cos((double)(2 * Mathf.PI * frequency * phase + Mathf.Pow((double)1.618034, j - 1)))));
            // harsh squares depending on frequency: data[i] += (double)(gain * (Mathf.Cos((double)(2 * Mathf.PI * frequency * phase + z))));
        }
    }
}