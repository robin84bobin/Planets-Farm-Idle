namespace Game.Runtime.Presentation.InfoPopup
{
    public class InfoPopupPresenter
    {
        public string InfoText { get; }

        public InfoPopupPresenter(string infoText)
        {
            InfoText = infoText;
        }
    }
}