using FlickrNet;
using VVVV.PluginInterfaces.V2;

namespace FFFFlickr
{
	[PluginInfo(Name = "GetPhotoMediumUrl", Author = "alg", Category = "Flickr", Help = "Get photo medium url")]
	public class GetPhotoMediumUrlNode : GetPhotoUrl
	{
		public override void Evaluate(int spreadMax)
		{
			FUrlOutput.SliceCount = spreadMax;
			FWidthOutput.SliceCount = spreadMax;
			FHeightOutput.SliceCount = spreadMax;
			
			for (int i = 0; i < spreadMax; i++)
			{
				Photo photo = FPhotoInput[i];
				
				if(photo == null) continue;

				FUrlOutput[i] = photo.MediumUrl;
				
				if (photo.MediumWidth != null)
				{
					FWidthOutput[i] = (int)photo.MediumWidth;
				}
				else
				{
					FWidthOutput[i] = 0;
				}
					
				if (photo.MediumHeight != null)
				{
					FHeightOutput[i] = (int) photo.MediumHeight;
				}
				else
				{
					FHeightOutput[i] = 0;
				}
			}
		}
	}
}
