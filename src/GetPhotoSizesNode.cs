using System;
using System.Collections.Generic;
using System.Threading;
using FlickrNet;
using VVVV.PluginInterfaces.V2;

namespace FFFFlickr
{
	[PluginInfo(Name = "GetSizes", Author = "alg", Category = "Flickr", Help = "Get available sizes of photo")]
	public class GetPhotoSizesNode : IPluginEvaluate
	{
		[Input("Connection", IsSingle = true)]
		private ISpread<Flickr> FConnectionInput;

		[Input("Photo")]
		private ISpread<Photo> FPhotoInput;

		[Input("Refresh", IsBang = true)]
		private ISpread<bool> FRefreshInput;

		[Output("Width")] 
		private ISpread<ISpread<int>> FWidthOutput;

		[Output("Height")] 
		private ISpread<ISpread<int>> FHeightOutput;

		[Output("Url")]
		private ISpread<ISpread<String>> FUrlOutput;
 
		[Output("Label")]
		private ISpread<ISpread<String>> FLabelOutput;

		[Output("Source")]
		private ISpread<ISpread<String>> FSourceOutput;

		[Output("MediaType")]
		private ISpread<ISpread<String>> FMediaTypeOutput;

		private readonly List<SizeCollection> FSizesCollections = new List<SizeCollection>();

		private int FSpreadMax;
		private Thread FThread;

		public void Evaluate(int spreadMax)
		{
			FSpreadMax = spreadMax;

			FWidthOutput.SliceCount = spreadMax;
			FHeightOutput.SliceCount = spreadMax;
			FUrlOutput.SliceCount = spreadMax;
			FLabelOutput.SliceCount = spreadMax;
			FSourceOutput.SliceCount = spreadMax;
			FMediaTypeOutput.SliceCount = spreadMax;

			if (FRefreshInput[0])
			{
				if (FThread != null) return;

				FThread = new Thread(GetData);
				FThread.Start();
			}

			AssignData();
		}

		private void GetData()
		{
			Flickr connection = FConnectionInput[0];
			FSizesCollections.Clear();

			for (int i = 0; i < FSpreadMax; i++)
			{
				try
				{
					SizeCollection sizes = connection.PhotosGetSizes(FPhotoInput[i].PhotoId);
					FSizesCollections.Add(sizes);
					
				}
				catch
				{
				}
			}
		}

		private void AssignData()
		{
			if(FSizesCollections.Count < FSpreadMax) return;

			for (int i = 0; i < FSpreadMax; i++)
			{
				

				SizeCollection sizes = FSizesCollections[i];
				int count = sizes.Count;

				FWidthOutput[i].SliceCount = count;
				FHeightOutput[i].SliceCount = count;
				FUrlOutput[i].SliceCount = count;
				FLabelOutput[i].SliceCount = count;
				FSourceOutput[i].SliceCount = count;
				FMediaTypeOutput[i].SliceCount = count;

				for (int j = 0; j < sizes.Count; j++)
				{
					Size size = sizes[j];

					FWidthOutput[i][j] = size.Width;
					FHeightOutput[i][j] = size.Height;
					FUrlOutput[i][j] = size.Url;
					FLabelOutput[i][j] = size.Label;
					FSourceOutput[i][j] = size.Source;
					FMediaTypeOutput[i][j] = size.MediaType.ToString();
				}
			}
			
		}
	}
}
