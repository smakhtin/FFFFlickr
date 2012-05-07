using FlickrNet;
using VVVV.PluginInterfaces.V2;

namespace FFFFlickr
{

	[PluginInfo(Name = "GetPhotoWebUrl", Author = "alg", Category = "Flickr", Help = "Get photo web url")]
	public class GetPhotoWebUrlNode : IPluginEvaluate
	{
		[Input("Photo")] 
		private ISpread<Photo> FPhotoInput;
		
		[Output("Url")] 
		private ISpread<string> FUrlOutput;
		
		public void Evaluate(int spreadMax)
		{
			FUrlOutput.SliceCount = spreadMax;

			for (int i = 0; i < spreadMax; i++)
			{
				Photo photo = FPhotoInput[i];

				if (photo == null) continue;

				FUrlOutput[i] = photo.WebUrl;
			}
		}
	}
}
