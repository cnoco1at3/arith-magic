using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Util;

namespace SoundLib {
    public class SoundManager : PersistentSingleton<SoundManager> {

        private static AudioSource bgm_src_;
        private static AudioSource[] sfx_src_;

        // this would allow at most 3 sound effects playing simultaneously
        private const int kSFXBufferSize = 6;

        // Use this for initialization
        void Start() {
            bgm_src_ = gameObject.AddComponent<AudioSource>();
            bgm_src_.loop = true;
            sfx_src_ = new AudioSource[kSFXBufferSize];
            for (int i = 0; i < sfx_src_.Length; ++i)
                sfx_src_[i] = gameObject.AddComponent<AudioSource>();
        }

        public virtual void PlayBGM(AudioClip clip) {
            if (bgm_src_ == null) {
                bgm_src_ = gameObject.AddComponent<AudioSource>();
                bgm_src_.loop = true;
            }

            if (clip == null)
                return;
            if (bgm_src_.isPlaying)
                bgm_src_.Stop();
            bgm_src_.clip = clip;
            bgm_src_.Play();
        }

        // return true if successfully play a clip
        public virtual bool PlaySFX(AudioClip clip, bool loopSound = false, float vol = 1f) {
            if (clip == null)
                return false;
            try {
                for (int i = 0; i < sfx_src_.Length; ++i) {
                    if (sfx_src_[i] == null)
                        sfx_src_[i] = gameObject.AddComponent<AudioSource>();

                    AudioSource src = sfx_src_[i];
                    if (!src.isPlaying) {
                        src.clip = clip;
                        src.loop = loopSound;
                        src.volume = vol;
                        src.Play();
                        return true;
                    }
                }
            } catch (NullReferenceException) { }
            return false;
        }

        public virtual bool StopSFX(AudioClip clip) {
            if (clip == null)
                return false;
            try {
                for (int i = 0; i < sfx_src_.Length; ++i) {
                    if (sfx_src_[i] == null)
                        sfx_src_[i] = gameObject.AddComponent<AudioSource>();

                    AudioSource src = sfx_src_[i];
                    if (src.isPlaying && src.clip == clip) {
                        src.loop = false;
                        src.Stop();
                        return true;
                    }
                }
            } catch (NullReferenceException) { }
            return false;
        }
    }
}
