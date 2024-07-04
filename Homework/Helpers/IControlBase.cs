namespace Test
{
    public interface IControlBase
    {
        void Click();

        string GetText();

        bool Enabled { get; }

        bool Visible { get; }
    }
}
