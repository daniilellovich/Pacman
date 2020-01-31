using System;
using Un4seen.Bass;

namespace Pacman
{
    public static class SoundController
    {
        public static bool _soundON = true;
        static int _stream;
        static System.Media.SoundPlayer _sound;

        static SoundController()
            => Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);

        public static void PlaySound(string fileName)
        { 
            if (_soundON)
            {
                fileName = "Sounds\\" + fileName + ".wav";
                _stream = Bass.BASS_StreamCreateFile(fileName, 0, 0, BASSFlag.BASS_DEFAULT);
                Bass.BASS_ChannelPlay(_stream, false);
            }
        }

        public static void PlayLongSound(string fileName)
        {
            if (_soundON)
            {
                fileName = "Sounds\\" + fileName + ".wav";
                _sound = new System.Media.SoundPlayer(fileName);
                _sound.PlayLooping();
            }
        }

        public static void StopLongSound()
        {
            if (_soundON)
                _sound.Stop();
        }
    }
}