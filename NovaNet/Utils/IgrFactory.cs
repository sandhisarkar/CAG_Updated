/*
 * Created by SharpDevelop.
 * User: RahulN
 * Date: 03/06/2009
 * Time: 12:12 PM
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Windows.Forms;
namespace NovaNet
{
	namespace Utils
	{
	/// <summary>
	/// Imagery factory class, returning relevant types of Imagery objects as required
	/// </summary>
		public class IgrFactory
		{
			//Void constructor, not really required
			public IgrFactory()
			{
			}
			//Static method to return a requested type of Imagery object
			public static Imagery GetImagery(short prmType)
			{
				Imagery igr = null;
				try
				{
					switch(prmType)
					{
						case Constants.IGR_GDPICTURE:
							igr = new ci();
							break;
						case Constants.IGR_CLEARIMAGE:
							igr = new ci();
							break;
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message,"Error - Imagery",MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
				return igr;
			}
		}
	}
}
