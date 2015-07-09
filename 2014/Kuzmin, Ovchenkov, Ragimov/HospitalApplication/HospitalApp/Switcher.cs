using System.Windows.Controls;

namespace HospitalApp
{
  	public static class Switcher
  	{
    	public static MainWindow PageSwitcher;

    	public static void Switch(Page newPage)
    	{
      		PageSwitcher.Navigate(newPage);
    	}
  	}
}

