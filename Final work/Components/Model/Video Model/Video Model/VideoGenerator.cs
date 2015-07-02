using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Splicer.Timeline;
using Splicer.Renderer;
using Splicer.WindowsMedia;
using System.Drawing;

namespace Video_Model
{
    public class VideoModel
    {
        public void start()
        {
            using (ITimeline timeline = new DefaultTimeline(25))
            {
                IGroup group = timeline.AddVideoGroup(32, 160, 100);

                ITrack videoTrack = group.AddTrack();
                //test images
                IClip clip1 = videoTrack.AddImage(@"C:\Users\COS301\Documents\Visual Studio 2013\Projects\images\1.jpg", 0, 2);
                IClip clip2 = videoTrack.AddImage(@"C:\Users\COS301\Documents\Visual Studio 2013\Projects\images\2.jpg", 0, 2);
                IClip clip3 = videoTrack.AddImage(@"C:\Users\COS301\Documents\Visual Studio 2013\Projects\images\3.jpg", 0, 2);
                IClip clip4 = videoTrack.AddImage(@"C:\Users\COS301\Documents\Visual Studio 2013\Projects\images\4.jpg", 0, 2);

                double halfDuration = 0.5;

                group.AddTransition(clip2.Offset - halfDuration, halfDuration, StandardTransitions.CreateFade(), true);
                group.AddTransition(clip2.Offset, halfDuration, StandardTransitions.CreateFade(), false);

                group.AddTransition(clip3.Offset - halfDuration, halfDuration, StandardTransitions.CreateFade(), true);
                group.AddTransition(clip3.Offset, halfDuration, StandardTransitions.CreateFade(), false);

                group.AddTransition(clip4.Offset - halfDuration, halfDuration, StandardTransitions.CreateFade(), true);
                group.AddTransition(clip4.Offset, halfDuration, StandardTransitions.CreateFade(), false);

                ITrack audioTrack = timeline.AddAudioGroup().AddTrack();
                // test audio track
                IClip audio = audioTrack.AddAudio(@"C:\Users\COS301\Documents\Visual Studio 2013\Projects\images\Kalimba.mp3", 0, videoTrack.Duration);

                audioTrack.AddEffect(0, audio.Duration, StandardEffects.CreateAudioEnvelope(1.0, 1.0, 1.0, audio.Duration));

                using (
                    WindowsMediaRenderer renderer =
                        new WindowsMediaRenderer(timeline, @"C:\Users\COS301\Documents\Visual Studio 2013\Projects\images\output.wmv", WindowsMediaProfiles.HighQualityVideo))
                {
                    renderer.Render();
                }
            }
        }
    }
}
