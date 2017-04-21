using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Util;

namespace SoundLib {
    public class SoundManager : PersistentSingleton<SoundManager> {

        private static AudioSource bgm_src_;
        private static AudioSource[] sfx_src_;
        private static Queue<AudioClip> bgm_queue_;
        private static Queue<AudioClip> sfx_queue_;

        // this would allow at most 3 sound effects playing simultaneously
        private const int kSFXBufferSize = 3;

        // Use this for initialization
        void Start() {
            bgm_src_ = gameObject.AddComponent<AudioSource>();
            bgm_src_.playOnAwake = false;
            bgm_src_.loop = true;

            sfx_src_ = new AudioSource[kSFXBufferSize];
            for (int i = 0; i < sfx_src_.Length; ++i) {
                sfx_src_[i] = gameObject.AddComponent<AudioSource>();
                sfx_src_[i].playOnAwake = false;
            }

            if (bgm_queue_ == null)
                bgm_queue_ = new Queue<AudioClip>();
            FlushBGM();

            if (sfx_queue_ == null)
                sfx_queue_ = new Queue<AudioClip>();
            FlushSFX();
        }

        public void SwitchScene(AudioClip bgm, AudioClip[] sfx) {
            StopBGM();
            StopSFX();
            PlayBGM(bgm);
            PlaySFX(sfx);
        }

        public void SwitchScene(AudioClip bgm, AudioClip sfx) {
            StopBGM();
            StopSFX();
            PlayBGM(bgm);
            PlaySFX(sfx);
        }

        public void FlushBGM() {
            AudioClip bgm = null;
            while (bgm_queue_.Count > 0)
                bgm = bgm_queue_.Dequeue();
            PlayBGM(bgm);
        }

        public void FlushSFX() {
            bool blocked = false;
            while (!blocked && sfx_queue_.Count > 0)
                blocked = PlaySFX(sfx_queue_.Dequeue());
        }

        public bool PlayBGM(AudioClip clip) {
            if (bgm_src_ == null) {
                bgm_src_ = gameObject.AddComponent<AudioSource>();
                bgm_src_.loop = true;
                bgm_src_.playOnAwake = false;
            }

            if (clip == null)
                return false;

            try {
                if (bgm_src_.isPlaying)
                    bgm_src_.Stop();
                bgm_src_.clip = clip;
                bgm_src_.Play();

                return true;
            } catch (NullReferenceException e) {
                try {
                    bgm_queue_.Enqueue(clip);
                } catch (NullReferenceException) {
                    bgm_queue_ = new Queue<AudioClip>();
                    bgm_queue_.Enqueue(clip);
                }
                Debug.LogException(e);
            }
            return false;
        }

        public bool StopBGM() {
            try {
                if (bgm_src_.isPlaying) {
                    bgm_src_.Stop();
                    return true;
                }
            } catch (NullReferenceException e) {
            }
            return false;
        }

        // return true if successfully play a clip
        public bool PlaySFX(AudioClip clip, bool loop = false) {
            if (clip == null)
                return false;

            try {
                for (int i = 0; i < sfx_src_.Length; ++i) {
                    if (sfx_src_[i] == null) {
                        sfx_src_[i] = gameObject.AddComponent<AudioSource>();
                        sfx_src_[i].playOnAwake = false;
                    }

                    AudioSource src = sfx_src_[i];
                    if (!src.isPlaying) {
                        src.clip = clip;
                        src.loop = loop;
                        src.Play();

                        return true;
                    }
                }
            } catch (NullReferenceException e) {
                try {
                    sfx_queue_.Enqueue(clip);
                } catch (NullReferenceException) {
                    sfx_queue_ = new Queue<AudioClip>();
                    sfx_queue_.Enqueue(clip);
                }
            }
            return false;
        }

        public bool PlaySFX(AudioClip[] clips, bool loop = false) {
            if (clips == null)
                return false;
            foreach (AudioClip clip in clips)
                if (!PlaySFX(clip, loop))
                    return false;
            return true;
        }

        // return true if successfully stop a clip
        public bool StopSFX(AudioClip clip) {
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
            } catch (NullReferenceException e) {
            }
            return false;
        }

        public bool StopSFX() {
            try {
                foreach (AudioSource src in sfx_src_) {
                    src.Stop();
                }
                return true;
            } catch (NullReferenceException e) { }
            return false;
        }
    }
}
