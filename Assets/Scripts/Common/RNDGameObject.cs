using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Options;

public class RNDGameObject : MonoBehaviour
{
    [SerializeField] private Text txtMusicTime;
    [SerializeField] private AudioSource clapSource;

    private AudioSource musicSource;
    private SMFile sm;
    private float stdBpm = 60f;
    private float offsetSample;
    private float nextSample;
    private float samplesPerBeat;
    private bool isStart;
    private const float defaultOffset = 0.05f;
    private int underBeats = 4;
    private int upperBeats = 2;

    private void Start()
    {
        clapSource.clip = FileReader.Instance.LoadFile<AudioClip>("Clap", FILEPATH.COMMONMUSIC_PATH);
        musicSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!isStart) return;

        // �� ����ð� * ���ļ��� ���� ���κ��� ũ�ų� ������ �۵�.
        if (musicSource.timeSamples >= nextSample)
        {
            StartCoroutine(PlayTicks());
        }
    }

    public void OnClickPlay()
    {
        sm = FileReader.Instance.ReadGameFile("Rrhar'il");
        if (sm == null) return;

        musicSource.clip = sm.music;
        CalculateSync();

        musicSource.Play();
        isStart = true;
    }

    // ������ ��α� : https://healp.tistory.com/8
    private void CalculateSync()
    {
        // �� ������ �ð� * ���ļ� (���� bpm�� ����, ���ڿ� ���� �ٲ��.
        // ����� (����BPM(60) / ���� BPM) * ����(4/4=1) * ���� ���ļ��� ����Ѵ�)
        samplesPerBeat = (stdBpm / sm.bpms[0][0]) * sm.beat * musicSource.clip.frequency;
        // ���� ���������� ���ļ��� ����. (���� ���� �������� ��߳��� 0.05�������� ���ؾ� �´�)
        offsetSample = musicSource.clip.frequency * (sm.offset + defaultOffset);
        // ���� ���� �ð� * ���ļ�
        nextSample = samplesPerBeat - offsetSample;
    }

    private IEnumerator PlayTicks()
    {
        clapSource.Play();
        nextSample += (stdBpm / sm.bpms[0][0]) * musicSource.clip.frequency;
        txtMusicTime.text = $"BPM - {sm.bpms[0][0]} : {upperBeats++} / {underBeats}";
        if (upperBeats > underBeats) upperBeats = 1;

        yield return null;
    }
}
