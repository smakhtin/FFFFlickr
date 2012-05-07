using FlickrNet;
using VVVV.PluginInterfaces.V2;

namespace FFFFlickr
{
	[PluginInfo(Name = "GetPhotoThumbnailUrl", Author = "alg", Category = "Flickr", Help = "Get photo thumbnail url")]
	public class GetPhotoThumbnailUrl : GetPhotoUrl
	{
		public override void Evaluate(int spreadMax)
		{
			FUrlOutput.SliceCount = spreadMax;

			for (int i = 0; i < spreadMax; i++)
			{
				Photo photo = FPhotoInput[i];

				if(photo == null) continue;

				FUrlOutput[i] = FPhotoInput[i].ThumbnailUrl;

				if (photo.ThumbnailWidth != null)
				{
					FWidthOutput[i] = (int)photo.ThumbnailWidth;
				}
				else
				{
					FWidthOutput[i] = 0;
				}

				if (photo.ThumbnailHeight != null)
				{
					FHeightOutput[i] = (int)photo.ThumbnailHeight;
				}
				else
				{
					FHeightOutput[i] = 0;
				}
			}
			
		}
	}
}
