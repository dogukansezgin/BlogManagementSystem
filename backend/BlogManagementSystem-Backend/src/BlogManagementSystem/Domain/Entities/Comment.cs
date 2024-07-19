using System.Xml.Linq;

namespace Domain.Entities;

public class Comment : NArchitecture.Core.Persistence.Repositories.Entity<Guid>
{
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public Guid BlogPostId { get; set; }
    public Guid? ParentId { get; set; }

    public virtual User User { get; set; }
    public virtual BlogPost BlogPost { get; set; }
    public virtual Comment Parent { get; set; }
    public virtual ICollection<Comment> Replies { get; set; }

    public Comment()
    {
        Replies = new HashSet<Comment>();
    }

    public Comment(Guid id, string content, Guid userId, Guid blogPostId, Guid parentId)
    {
        Id = id;
        Content = content;
        UserId = userId;
        BlogPostId = blogPostId;
        ParentId = parentId;
    }
}
