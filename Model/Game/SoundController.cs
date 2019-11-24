using System;
using System.IO;
using Un4seen.Bass;

namespace Pacman
{
    public static class SoundController
    {
        public static bool _soundON = true;
        static int _stream;
        static System.Media.SoundPlayer sound;

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

        public static void PlayLongSound(Stream _soundFile)
        {
            if (_soundON)
            {
                sound = new System.Media.SoundPlayer(_soundFile);
                sound.PlayLooping();
            }
        }

        public static void StopLongSound()
        {
            if (_soundON)
                sound.Stop();
        }
    }
}