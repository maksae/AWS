using System;

namespace AWS.S3
{
	class Program
	{
        static void Main(string[] args)
		{
            S3FileManager s3Manager = new S3FileManager();
            string fileName = "TestFile2.txt";

            Console.WriteLine(s3Manager.CheckFileinBucket(fileName));

            Console.WriteLine("Press any key to upload {0} file", fileName);
            Console.ReadKey();
            using (var fileStream = FileHelper.GetFileStream(fileName))
            {
                Console.WriteLine(s3Manager.SendFileToBucket(fileName, fileStream));
            }

            Console.WriteLine(s3Manager.CheckFileinBucket(fileName));

            Console.WriteLine("Press any key to download {0} file", fileName);
            Console.ReadKey();
            Console.WriteLine(s3Manager.GetFileFromBucket(fileName));

            Console.WriteLine("Press any key to delete {0} file", fileName);
            Console.ReadKey();
            Console.WriteLine(s3Manager.DeleteFile(fileName));

            Console.WriteLine(s3Manager.CheckFileinBucket(fileName));

            Console.ReadKey();
        }
    }
}
