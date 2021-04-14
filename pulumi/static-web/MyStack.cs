using Pulumi;
using Pulumi.Gcp.Storage;
using Pulumi.Gcp.Storage.Inputs;

class MyStack : Stack
{
    public MyStack()
    {
        // Create a GCP resource (Storage Bucket)
        var bucket = new Bucket("my-bucket", new BucketArgs
	{
    		Website = new BucketWebsiteArgs
    		{
        		MainPageSuffix = "index.html"
    		},
    		UniformBucketLevelAccess = true
	});
	var bucketIAMBinding = new BucketIAMBinding("my-bucket-IAMBinding", new BucketIAMBindingArgs
	{
    		Bucket = bucket.Name,
    		Role = "roles/storage.objectViewer",
    		Members = "allUsers"
	});
	var bucketObject = new BucketObject("index.html", new BucketObjectArgs
	{
		Bucket = bucket.Name,
		ContentType = "text/html",
		Source = new FileAsset("index.html")
	});
        // Export the DNS name of the bucket
        this.BucketName = bucket.Url;
	this.BucketEndpoint = Output.Format($"http://storage.googleapis.com/{bucket.Name}/{bucketObject.Name}");
    }

    [Output]
    public Output<string> BucketName { get; set; }
    [Output]
    public Output<string> BucketEndpoint { get; set; }
}
