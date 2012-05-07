using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VVVV.PluginInterfaces.V2;
using FlickrNet;

namespace FFFFlickr
{
	public abstract class GetPhotoUrl : IPluginEvaluate
	{
		[Input("Photo")]
		protected ISpread<Photo> FPhotoInput;
		[Output("Url", StringType = StringType.URL)]
		protected ISpread<string> FUrlOutput;
		[Output("Width")] protected ISpread<int> FWidthOutput;
		[Output("Height")] protected ISpread<int> FHeightOutput; 

		public virtual void Evaluate(int spreadMax)
		{
		}
	}
}
