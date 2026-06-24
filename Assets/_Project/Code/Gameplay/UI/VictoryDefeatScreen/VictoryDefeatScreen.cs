using _Project.Code.Gameplay.UI.Infrastructure;

namespace _Project.Code.Gameplay.UI.VictoryDefeat
{
    public sealed class VictoryDefeatScreen : BaseScreen<VictoryDefeatView, VictoryDefeatArgs>
    {
        private string VictoryTitle = "Вы победили!";
        private string DefeatTitle = "Вы не победели";
        
        private string VictoryDescText = ":)";
        private string DefeatDescText = ":(";
        
        private string BtText = "Еще раз!";
       
        
        protected override void OnOpen()
        {
            var titleText = Args.IsVictory ? VictoryTitle : DefeatTitle;
            var descText = Args.IsVictory ? VictoryDescText : DefeatDescText;
            
            View.SetView(titleText, BtText, descText);
            View.ContinueClicked = RequestClose;
        }

        protected override void OnClose()
        {
            View.ContinueClicked = null;
        }
    }
}
