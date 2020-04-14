using System;
using System.Linq;
using com.b_velop.Deploy_O_Mat.Web.Data.Context;
using com.b_velop.Deploy_O_Mat.Web.Domain.Models;

namespace com.b_velop.Deploy_O_Mat.Web.Data
{
    public class Seed
    {
        public static void SeedData(
            WebContext dataContext)
        {
            if (dataContext.DockerImages.Count() > 0)
                return;

            dataContext.AddRange(new[]
            {
                new DockerImage
                {
                        Id = Guid.Parse("956EA3D3-B054-40A0-B770-0BA7E22AFD5E"),
                        Created = DateTime.Parse("2020-03-01T17:52:45Z"),
                        Updated = DateTime.Parse("2020-03-05T19:49:36Z"),
                        Name = "angularair",
                        Tag = "latest",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/angularair",
                        RepoUrl = "https://hub.docker.com/r/millegalb/angularair",
                        BuildId = Guid.Parse("48dc7e68-1d28-4b0c-8f9b-ea014f591b68")
                },
                new DockerImage
                {
                        Id = Guid.Parse("7712048D-7209-4F8D-94B0-10887995D25D"),
                        Created = DateTime.Parse("2019-11-22T16:05:35Z"),
                        Updated = DateTime.Parse("2020-02-04T15:31:46Z"),
                        Name = "slipways.graphql",
                        Tag = "latest",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/slipways.graphq",
                        RepoUrl = "https://hub.docker.com/r/millegalb/slipways.graphql",
                        BuildId = Guid.Parse("b37e3ea3-0134-4c51-a03d-35c9ad1f78c9")
                },
                new DockerImage
                {
                        Id = Guid.Parse("8778DB42-363C-4088-B00E-129E5DB2C443"),
                        Created = DateTime.Parse("2019-11-28T21:50:43Z"),
                        Updated = DateTime.Parse("2019-11-28T21:50:43Z"),
                        Name = "stack.air.server",
                        Tag = "dev-latest",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/stack.air.server",
                        RepoUrl = "https://hub.docker.com/r/millegalb/stack.air.server",
                        BuildId = Guid.Parse("3dc49028-5ba5-4493-b43d-6ff75cddf991")
                },
                new DockerImage
                {
                        Id = Guid.Parse("75FAFA02-E4CB-462D-8940-1A896E05E420"),
                        Created = DateTime.Parse("2019-12-19T17:48:57Z"),
                        Updated = DateTime.Parse("2020-03-06T11:56:09Z"),
                        Name = "slipways.web",
                        Tag = "latest",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/slipways.web",
                        RepoUrl = "https://hub.docker.com/r/millegalb/slipways.web",
                        BuildId = Guid.Parse("b2de983d-2039-4980-9ca5-3420650b1b3c")
                },
                new DockerImage
                {
                        Id = Guid.Parse("B7BEE290-496D-4C0F-BE1D-213279EF847D"),
                        Created = DateTime.Parse("2019-11-21T20:42:25Z"),
                        Updated = DateTime.Parse("2019-11-22T09:46:12Z"),
                        Name = "stack.air",
                        Tag = "latest",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/stack.air",
                        RepoUrl = "https://hub.docker.com/r/millegalb/stack.air",
                        BuildId = Guid.Parse("13cbc187-4b74-4eab-97aa-f66a173869d9")
                },
                new DockerImage
                {
                        Id = Guid.Parse("045EB826-4797-4869-8336-346D7E3EFEDA"),
                        Created = DateTime.Parse("2019-12-05T21:32:43Z"),
                        Updated = DateTime.Parse("2019-12-05T21:32:43Z"),
                        Name = "identity.server",
                        Tag = "2",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/identity.server",
                        RepoUrl = "https://hub.docker.com/r/millegalb/identity.server",
                        BuildId = Guid.Parse("7f79d119-3310-4ecc-bb67-ce52803053e7")
                },
                new DockerImage
                {
                        Id = Guid.Parse("8A1874E2-C9D8-41EE-9583-4AA88ACC4076"),
                        Created = DateTime.Parse("2019-11-28T21:30:38Z"),
                        Updated = DateTime.Parse("2019-11-28T21:30:38Z"),
                        Name = "stack.air.server",
                        Tag = "0.0.1",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/stack.air.server",
                        RepoUrl = "https://hub.docker.com/r/millegalb/stack.air.server",
                        BuildId = Guid.Parse("bf8a8d94-f74a-4f43-a845-04a3445eed39")
                },
                new DockerImage
                {
                        Id = Guid.Parse("E11E5948-26D0-4702-B464-527EDECF2081"),
                        Created = DateTime.Parse("2019-11-25T13:29:09Z"),
                        Updated = DateTime.Parse("2020-03-06T12:22:41Z"),
                        Name = "slipways.web",
                        Tag = "dev-latest",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/slipways.web",
                        RepoUrl = "https://hub.docker.com/r/millegalb/slipways.web",
                        BuildId = Guid.Parse("548501d1-3934-4c0c-9dc3-d35ca1d60f9b")
                },
                new DockerImage
                {
                        Id = Guid.Parse("513D34B5-AA30-423E-AAA2-6425E547E767"),
                        Created = DateTime.Parse("2019-12-20T15:05:38Z"),
                        Updated = DateTime.Parse("2020-02-19T19:32:02Z"),
                        Name = "slipways.api",
                        Tag = "dev-latest",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/slipways.api",
                        RepoUrl = "https://hub.docker.com/r/millegalb/slipways.api",
                        BuildId = Guid.Parse("3b476325-7efd-4a6f-b533-f954513db35d")
                },
                new DockerImage
                {
                        Id = Guid.Parse("120475A5-1FAC-4FD2-9F48-326A58288D3B"),
                        Created = DateTime.Parse("2019-11-28T21:59:35Z"),
                        Updated = DateTime.Parse("2019-11-28T23:02:24Z"),
                        Name = "stack.air.server",
                        Tag = "latest",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/stack.air.server",
                        RepoUrl = "https://hub.docker.com/r/millegalb/stack.air.server",
                        BuildId = Guid.Parse("87d3345b-0a6a-49c4-a7a2-cd44d7ec4d14")
                },
                new DockerImage
                {
                    Id = Guid.Parse("8EF6AB16-B470-4691-AC4F-715E9A29CF93"),
                    Created = DateTime.Parse("2019-11-28T22:21:46Z"),
                    Updated = DateTime.Parse("2019-11-28T22:21:46Z"),
                    Name = "stack.graphql",
                    Tag = "latest",
                    Pusher = "millegalb",
                    Namespace = "millegalb",
                    Owner = "millegalb",
                    RepoName = "millegalb/stack.graphql",
                    RepoUrl = "https://hub.docker.com/r/millegalb/stack.graphql",
                    BuildId = Guid.Parse("4ab2b600-578e-4f78-a50f-b18414fff75a")
                },
                new DockerImage
                {
                    Id = Guid.Parse("CE197ECD-2225-430A-8611-740A6B7ACEBD"),
                    Created = DateTime.Parse("2019-11-23T11:19:05Z"),
                    Updated = DateTime.Parse("2020-03-22T12:49:07Z"),
                    Name = "feinstaubserver",
                    Tag = "latest",
                    Pusher = "mbodenstein",
                    Namespace = "mbodenstein",
                    Owner = "mbodenstein",
                    RepoName = "mbodenstein/feinstaubserver",
                    RepoUrl = "https://hub.docker.com/r/mbodenstein/feinstaubserver",
                    BuildId = Guid.Parse("e0fae9a5-bf93-4dd3-b970-dfe92ba68d7f")
                },
                new DockerImage
                {
                    Id = Guid.Parse("74BEBDEB-C923-423E-BCA1-78026645F37E"),
                    Created = DateTime.Parse("2019-11-23T11:41:41Z"),
                    Updated = DateTime.Parse("2019-11-23T14:49:04Z"),
                    Name = "feinstaubserver",
                    Tag = "1.0.11",
                    Pusher = "mbodenstein",
                    Namespace = "mbodenstein",
                    Owner = "mbodenstein",
                    RepoName = "mbodenstein/feinstaubserver",
                    RepoUrl = "https://hub.docker.com/r/mbodenstein/feinstaubserver",
                    BuildId = Guid.Parse("6f8ce26c-b8da-4de7-a5f8-627a1c57b186")
                },
                new DockerImage
                {
                    Id = Guid.Parse("2D8111DD-D5C3-49DC-B36B-8EF4C06314EC"),
                    Created = DateTime.Parse("2019-12-03T07:31:15Z"),
                    Updated = DateTime.Parse("2020-01-02T22:34:21Z"),
                    Name = "identity.server",
                    Tag = "dev-latest",
                    Pusher = "millegalb",
                    Namespace = "millegalb",
                    Owner = "millegalb",
                    RepoName = "millegalb/identity.server",
                    RepoUrl = "https://hub.docker.com/r/millegalb/identity.server",
                    BuildId = Guid.Parse("09763b26-8baf-4805-a12f-5efadd2861dd")
                },
                new DockerImage
                {
                    Id = Guid.Parse("B0867C89-5F80-4E92-9507-92C1799575CC"),
                    Created = DateTime.Parse("2019-11-21T20:32:29Z"),
                    Updated = DateTime.Parse("2020-01-20T16:04:52Z"),
                    Name = "testpoint",
                    Tag = "latest",
                    Pusher = "millegalb",
                    Namespace = "millegalb",
                    Owner = "millegalb",
                    RepoName = "millegalb/testpoint",
                    RepoUrl = "https://hub.docker.com/r/millegalb/testpoint",
                    BuildId = Guid.Parse("4b16343d-b4ea-4d6e-8d99-eca0df789291")
                },
                new DockerImage
                {
                    Id = Guid.Parse("5AA4B02C-EF68-4BE5-A2CC-979BB1AA075D"),
                    Created = DateTime.Parse("2020-02-29T23:59:20Z"),
                    Updated = DateTime.Parse("2020-02-29T23:59:20Z"),
                    Name = "angularair",
                    Tag = "0.0.2",
                    Pusher = "millegalb",
                    Namespace = "millegalb",
                    Owner = "millegalb",
                    RepoName = "millegalb/angularair",
                    RepoUrl = "https://hub.docker.com/r/millegalb/angularair",
                    BuildId = Guid.Parse("52c60dcd-a1d5-423f-b442-31905c75023d")
                },
                new DockerImage
                {
                    Id = Guid.Parse("A464E48E-7592-4EBA-9AEB-9E76FD046CBB"),
                    Created = DateTime.Parse("2019-12-26T16:08:47Z"),
                    Updated = DateTime.Parse("2020-02-18T10:00:35Z"),
                    Name = "slipways.api",
                    Tag = "latest",
                    Pusher = "millegalb",
                    Namespace = "millegalb",
                    Owner = "millegalb",
                    RepoName = "millegalb/slipways.api",
                    RepoUrl = "https://hub.docker.com/r/millegalb/slipways.api",
                    BuildId = Guid.Parse("891c542c-02ae-404c-b4ed-7ab40f0ea731")
                },
                new DockerImage
                {
                    Id = Guid.Parse("3EB65D3A-ACF2-4B5B-BD96-B529D290BF0C"),
                    Created = DateTime.Parse("2019-11-28T22:10:32Z"),
                    Updated = DateTime.Parse("2019-11-28T22:10:32Z"),
                    Name = "stack.graphql",
                    Tag = "dev-latest",
                    Pusher = "millegalb",
                    Namespace = "millegalb",
                    Owner = "millegalb",
                    RepoName = "millegalb/stack.graphql",
                    RepoUrl = "https://hub.docker.com/r/millegalb/stack.graphql",
                    BuildId = Guid.Parse("10bbff64-7fa3-4252-9b94-f0bcb8c85a06")
                },
                new DockerImage
                {
                    Id = Guid.Parse("B4762343-FFBC-4E62-880E-B6336A8ACF5D"),
                    Created = DateTime.Parse("2019-12-20T16:14:09Z"),
                    Updated = DateTime.Parse("2020-01-27T10:03:37Z"),
                    Name = "slipways.dataprovider",
                    Tag = "latest",
                    Pusher = "millegalb",
                    Namespace = "millegalb",
                    Owner = "millegalb",
                    RepoName = "millegalb/slipways.dataprovider",
                    RepoUrl = "https://hub.docker.com/r/millegalb/slipways.dataprovider",
                    BuildId = Guid.Parse("5d564490-0ace-4087-9118-c73de7295938")
                },
                new DockerImage
                {
                    Id = Guid.Parse("E52162E9-FDFC-4F00-A6E7-B7B4DBC11558"),
                    Created = DateTime.Parse("2019-12-05T21:55:59Z"),
                    Updated = DateTime.Parse("2019-12-05T21:55:59Z"),
                    Name = "identity.server",
                    Tag = "3",
                    Pusher = "millegalb",
                    Namespace = "millegalb",
                    Owner = "millegalb",
                    RepoName = "millegalb/identity.server",
                    RepoUrl = "https://hub.docker.com/r/millegalb/identity.server",
                    BuildId = Guid.Parse("b0073fd8-3938-4a99-874f-cfcc049ac361")
                },
                new DockerImage
                {
                        Id = Guid.Parse("94B2BD53-27DA-417E-AFE5-B9636A1FCC46"),
                        Created = DateTime.Parse("2020-03-01T00:10:41Z"),
                        Updated = DateTime.Parse("2020-03-01T00:10:41Z"),
                        Name = "angularair",
                        Tag = "0.0.3",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/angularair",
                        RepoUrl = "https://hub.docker.com/r/millegalb/angularair",
                        BuildId = Guid.Parse("ff92b587-d577-446c-b454-fbf15ce632f5")
                },
                new DockerImage
                {
                        Id = Guid.Parse("FEF75850-8E20-46C0-A07F-C356EE1487B2"),
                        Created = DateTime.Parse("2019-11-26T12:48:54Z"),
                        Updated = DateTime.Parse("2019-11-26T12:48:53Z"),
                        Name = "feinstaubserver",
                        Tag = "dev-latest",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/feinstaubserver",
                        RepoUrl = "https://hub.docker.com/r/millegalb/feinstaubserver",
                        BuildId = Guid.Parse("63affa73-df6b-47a1-b0a0-f4cd38e54470")
                },
                new DockerImage
                {
                        Id = Guid.Parse("72248024-59A6-4777-89D4-CC4F913FFBC0"),
                        Created = DateTime.Parse("2019-12-05T21:09:00Z"),
                        Updated = DateTime.Parse("2019-12-05T21:08:58Z"),
                        Name = "identity.server",
                        Tag = "1",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/identity.server",
                        RepoUrl = "https://hub.docker.com/r/millegalb/identity.server",
                        BuildId = Guid.Parse("8b5b7d0b-f6fe-40c6-b027-7e65109cdcfb")
                },
                new DockerImage
                {
                        Id = Guid.Parse("9A6BFC85-236D-4F17-A742-D7E3971DD82A"),
                        Created = DateTime.Parse("2019-12-20T15:15:46Z"),
                        Updated = DateTime.Parse("2020-01-27T06:55:04Z"),
                        Name = "slipways.dataprovider",
                        Tag = "dev-latest",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/slipways.dataprovider",
                        RepoUrl = "https://hub.docker.com/r/millegalb/slipways.dataprovider",
                        BuildId = Guid.Parse("fc141650-5326-498f-91d2-47bad4035544")
                },
                new DockerImage
                {
                        Id = Guid.Parse("BD137691-7CA1-4E44-AB26-DDF6393AEEED"),
                        Created = DateTime.Parse("2019-12-05T22:12:55Z"),
                        Updated = DateTime.Parse("2019-12-05T22:12:55Z"),
                        Name = "identity.server",
                        Tag = "5",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/identity.server",
                        RepoUrl = "https://hub.docker.com/r/millegalb/identity.server",
                        BuildId = Guid.Parse("525f2134-5e31-4e8b-b73f-7a23a8ce0352")
                },
                new DockerImage
                {
                        Id = Guid.Parse("5F76DCA8-0291-402F-86B3-E09D1FF06BE1"),
                        Created = DateTime.Parse("2019-12-05T22:10:19Z"),
                        Updated = DateTime.Parse("2019-12-05T22:10:14Z"),
                        Name = "identity.server",
                        Tag = "4",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/identity.server",
                        RepoUrl = "https://hub.docker.com/r/millegalb/identity.server",
                        BuildId = Guid.Parse("7a98c25f-4b5d-4c04-91a9-02c717a84524")
                },
                new DockerImage
                {
                        Id = Guid.Parse("76FA5C63-004A-4D3C-B737-F1244BA3B887"),
                        Created = DateTime.Parse("2019-11-27T07:45:50Z"),
                        Updated = DateTime.Parse("2020-02-04T15:22:50Z"),
                        Name = "slipways.graphql",
                        Tag = "dev-latest",
                        Pusher = "millegalb",
                        Namespace = "millegalb",
                        Owner = "millegalb",
                        RepoName = "millegalb/slipways.graphql",
                        RepoUrl = "https://hub.docker.com/r/millegalb/slipways.graphql",
                        BuildId = Guid.Parse("bb5c4a6a-df7a-4305-afc4-962ca028e0d9")
                }
    });
            dataContext.SaveChanges();
        }
    }
}
