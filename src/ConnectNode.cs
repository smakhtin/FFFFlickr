using System;
using FlickrNet;
using VVVV.PluginInterfaces.V2;

namespace FFFFlickr
{
	[PluginInfo(Name = "Connect", Author = "alg", Category = "Flickr", Help = "Connect to Flickr", AutoEvaluate = true)]
	public class ConnectNode : IPluginEvaluate
	{
		[Input("Init", IsBang = true, IsSingle = true)] 
		private ISpread<bool> FInitInput;
		
		[Output("Connection")] 
		private ISpread<Flickr> FConnectionOutput;

		[Output("Status")] 
		private ISpread<string> FStatusOutput;

		private Flickr FFlickr;
		private string FStatus = "null";

		public ConnectNode()
		{
			Init();
		}

		public void Evaluate(int spreadMax)
		{
			if(FInitInput[0]) Init();

			FStatusOutput[0] = FStatus;
			FConnectionOutput[0] = FFlickr;
		}

		private void Init()
		{
			try
			{
				FFlickr = new Flickr("5ca4f83e1cbfbfa12ba77adc07570cd0", "5c1e07a173e23229");
				FStatus = "Connected";
			}
			catch (Exception)
			{

				FStatus = "Can't connect";
			}
		}
	}
}
