using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Util;

namespace SoundLib {
    public class SoundManager : PersistentSingleton<SoundManager> {

        private static AudioSource bgm_src_;
        private static AudioSource[] sfx_src_;

        // this would allow at most 3 sound effects playing simultaneously
        private const int kSFXBufferSize = 3;

        // Use this for initialization
        void Start() {
            bgm_src_ = gameObject.AddComponent<AudioSource>();
            sfx_src_ = new AudioSource[kSFXBufferSize];
            for (int i = 0; i < sfx_src_.Length; ++i)
                sfx_src_[i] = gameObject.AddComponent<AudioSource>();
        }

        public virtual void PlayBGM(AudioClip clip) {
            bgm_src_.clip = clip;
            bgm_src_.Play();
        }

        // return true if successfully play a clip
        public virtual bool PlaySFX(AudioClip clip) {
            foreach (AudioSource src in sfx_src_) {
                if (!src.isPlaying) {
                    src.clip = clip;
                    src.Play();
                    return true;
                }
            }
            return false;
        }
    }
}
