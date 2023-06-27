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

        // 곡 진행시간 * 주파수가 다음 라인보다 크거나 같으면 작동.
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

    // 참고한 블로그 : https://healp.tistory.com/8
    private void CalculateSync()
    {
        // 한 마디의 시간 * 주파수 (값은 bpm에 따라, 박자에 따라 바뀐다.
        // 현재는 (기준BPM(60) / 곡의 BPM) * 박자(4/4=1) * 곡의 주파수로 계산한다)
        samplesPerBeat = (stdBpm / sm.bpms[0][0]) * sm.beat * musicSource.clip.frequency;
        // 곡의 시작지점을 주파수와 곱함. (현재 무슨 이유인지 어긋나서 0.05초정도를 더해야 맞다)
        offsetSample = musicSource.clip.frequency * (sm.offset + defaultOffset);
        // 다음 정박 시간 * 주파수
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
