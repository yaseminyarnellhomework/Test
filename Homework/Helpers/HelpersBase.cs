using System.Configuration;

namespace Test
{
    public class HelpersBase
    {
            
        public WaitHelpers WaitFor
        {
            get
            {
                return new WaitHelpers();
            }
        }

       
        public AppSettingsReader AppSettings
        {
            get
            {
                return new AppSettingsReader();
            }
        }

       

        public SeleniumHelpers SeleniumHelpers
        {
            get
            {
                return new SeleniumHelpers();
            }
        }

        public IframeHelpers IFrame
        {
            get
            {
                return new IframeHelpers();
            }
        }

        public object Javascript { get; internal set; }
    }
}
