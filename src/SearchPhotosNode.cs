using System.Collections.Generic;
using System.Threading;
using VVVV.PluginInterfaces.V2;
using FlickrNet;

namespace FFFFlickr
{
	[PluginInfo(Name = "SearchPhotos", Author = "alg", Category = "Flickr", Help = "Search photos by options on Flickr", AutoEvaluate = true)]
	public class SearchPhotosNode : IPluginEvaluate
	{
		[Input("Connection", IsSingle = true)] private ISpread<Flickr> FConnectionInput;

		[Input("Options")] private IDiffSpread<PhotoSearchOptions> FPhotoSearchOptionsInput;
		
		[Input("Refresh", IsBang = true)] private ISpread<bool> FRefreshInput;

		[Output("Photo Collection")]
		private ISpread<PhotoCollection> FPhotoCollectionsOutput;

		private readonly List<PhotoCollection> FPhotoCollections = new List<PhotoCollection>();
		private int FSpreadMax;
		private Thread FThread;

		public void Evaluate(int spreadMax)
		{
			FSpreadMax = spreadMax;

			if (FRefreshInput[0])
			{
				if(FThread != null) return;
				FThread = new Thread(GetData);
				FThread.Start();
			}

			//Output data
			FPhotoCollectionsOutput.AssignFrom(FPhotoCollections);
		}

		private void GetData()
		{
			FPhotoCollections.Clear();
			
			for (int i = 0; i < FSpreadMax; i++)
			{
				try
				{
					PhotoCollection collection = FConnectionInput[0].PhotosSearch(FPhotoSearchOptionsInput[i]);
					FPhotoCollections.Add(collection);
				}
				catch
				{
				}
			}

			FThread = null;
		}
	}
}
