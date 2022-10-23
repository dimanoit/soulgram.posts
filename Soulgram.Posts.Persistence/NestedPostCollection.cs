using System;

namespace Soulgram.Posts.Persistence;

public enum NestedPostCollection
{
    Comments,
    Likes
}

public static class NestedPostCollectionExtension
{
    public static string ToDbName(this NestedPostCollection collection)
    {
        switch (collection)
        {
            case NestedPostCollection.Comments:
                return "comments";
            case NestedPostCollection.Likes:
                return "likes";
        }

        throw new Exception();
    }
}