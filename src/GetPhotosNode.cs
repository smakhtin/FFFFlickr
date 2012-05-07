using VVVV.PluginInterfaces.V2;
using FlickrNet;

namespace FFFFlickr
{
	[PluginInfo(Name = "GetPhotos", Author = "alg", Category = "Flickr", Help = "Get all photos from photo collection", AutoEvaluate = true)]
	public class GetPhotosNode : IPluginEvaluate
	{
		[Input("Photo Collection")] private ISpread<PhotoCollection> FPhotoCollectionInput;
		[Output("Photo")] private ISpread<ISpread<Photo>> FPhotoOutput;

		public void Evaluate(int spreadMax)
		{
			FPhotoOutput.SliceCount = spreadMax;

			for (int i = 0; i < spreadMax; i++)
			{
				PhotoCollection collection = FPhotoCollectionInput[i];
				if (collection == null) return;

				FPhotoOutput[i].AssignFrom(collection);
			}
		}
	}
}
