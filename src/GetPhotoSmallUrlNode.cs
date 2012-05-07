using VVVV.PluginInterfaces.V2;
using FlickrNet;

namespace FFFFlickr
{
	[PluginInfo(Name = "GetPhotoSmallUrl", Author = "alg", Category = "Flickr", Help = "Get photo small url")]
	public class GetPhotoSmallUrlNode : GetPhotoUrl
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

				FUrlOutput[i] = photo.SmallUrl;

				if (photo.SmallWidth != null)
				{
					FWidthOutput[i] = (int) photo.SmallWidth;
				}
				else
				{
					FWidthOutput[i] = 0;
				}

				if (photo.SmallHeight != null)
				{
					FHeightOutput[i] = (int)photo.SmallHeight;
				}
				else
				{
					FHeightOutput[i] = 0;
				}
			}
		}
	}
}
