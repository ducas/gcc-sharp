using System.Windows.Forms;

namespace GccSharp.ConsoleApp.Confirmation
{
    public static class MessageBoxConfirmation
    {
        internal static bool Confirmation(Activity activity)
        {
            var messageBox = MessageBox.Show(activity.ToString(), "Entry Confirmation", MessageBoxButtons.YesNo);
            return messageBox == DialogResult.Yes;
        }
    }
}
