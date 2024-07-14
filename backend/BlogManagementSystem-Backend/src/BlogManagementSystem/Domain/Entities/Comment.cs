namespace Domain.Entities;

public class Comment : NArchitecture.Core.Persistence.Repositories.Entity<Guid>
{
    public string Content { get; set; }
    public Guid UserId { get; set; }
    public Guid BlogPostId { get; set; }

    public virtual User User { get; set; }
    public virtual BlogPost BlogPost { get; set; }

    public Comment()
    {
    }

    public Comment(Guid id, string content, Guid userId, Guid blogPostId)
    {
        Id = id;
        Content = content;
        UserId = userId;
        BlogPostId = blogPostId;
    }
}
