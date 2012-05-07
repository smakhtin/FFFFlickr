using System.Collections.Generic;
using VVVV.PluginInterfaces.V2;
using FlickrNet;

namespace FFFFlickr
{
	[PluginInfo(Name = "PhotoSearchOptions", Author = "alg", Category = "Flickr", Help = "Creates photo search options")]
	public class PhotoSearchOptionsNode : IPluginEvaluate
	{
		[Input("User Id")] private IDiffSpread<string> FUserIdInput;
		[Input("Tags")] private IDiffSpread<string> FTagsInput;
		[Input("Tag Mode")] private IDiffSpread<TagMode> FTagModeInput;
		[Input("Photos Per Page", DefaultValue = 100)] private IDiffSpread<int> FPerPageInput;

		[Output("Photo Search Options")] private ISpread<PhotoSearchOptions> FPhotoSearchOptionsOutput;
		[Output("Options Changed")] private ISpread<bool> FOptionsChangedOutput; 

		private readonly List<PhotoSearchOptions> FPhotoSearchOptions = new List<PhotoSearchOptions>();
		private bool FChanged;

		public void Evaluate(int spreadMax)
		{
			FChanged = false;

			if (FUserIdInput.IsChanged || FTagsInput.IsChanged || FTagsInput.IsChanged || FPerPageInput.IsChanged)
			{
				FPhotoSearchOptions.Clear();

				for (int i = 0; i < spreadMax; i++)	
				{
					PhotoSearchOptions options = new PhotoSearchOptions();
					
					string userId = FUserIdInput[i];
					if(!string.IsNullOrEmpty(userId)) options.UserId = userId;

					if(!string.IsNullOrEmpty(FTagsInput[i]))
					{
						options.Tags = FTagsInput[i];
					}

					options.PerPage = FPerPageInput[i];
					options.TagMode = FTagModeInput[i];

					FPhotoSearchOptions.Add(options);
				}

				FChanged = true;
			}

			FPhotoSearchOptionsOutput.AssignFrom(FPhotoSearchOptions);
			FOptionsChangedOutput[0] = FChanged;
		}
	}
}