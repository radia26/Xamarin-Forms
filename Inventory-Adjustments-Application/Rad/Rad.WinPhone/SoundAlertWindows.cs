using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;
using Rad.Utilities;

namespace Rad.WinPhone
{
    public class SoundAlertWindows : SoundAlertControl
    {


        static Stream stream1 = TitleContainer.OpenStream("soundeffect.wav");

        static SoundEffect sfx = SoundEffect.FromStream(stream1);

        static SoundEffectInstance soundEffect = sfx.CreateInstance();

        public override void PlayAlertSound()
        {
            try
            {
                FrameworkDispatcher.Update();

                soundEffect.Play();

            }
            catch (Exception ex)
            {
                string s = ex.InnerException.ToString();
            }


           


        }
    }
}
