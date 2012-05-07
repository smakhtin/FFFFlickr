using FlickrNet;
using VVVV.PluginInterfaces.V2;

namespace FFFFlickr
{
	[PluginInfo(Name = "GetPhotoOriginalUrl", Author = "alg", Category = "Flickr", Help = "Get photo original url")]
	public class GetPhotoOriginalUrlNode : GetPhotoUrl
	{
		public override void Evaluate(int spreadMax)
		{
			FUrlOutput.SliceCount = spreadMax;
			FWidthOutput.SliceCount = spreadMax;
			FHeightOutput.SliceCount = spreadMax;

			for (int i = 0; i < spreadMax; i++)
			{
				Photo photo = FPhotoInput[i];
				
				if (photo == null) continue;

				FUrlOutput[i] = photo.OriginalUrl;

				FWidthOutput[i] = photo.OriginalWidth;

				FHeightOutput[i] = photo.OriginalHeight;
			}
		}
	}
}
