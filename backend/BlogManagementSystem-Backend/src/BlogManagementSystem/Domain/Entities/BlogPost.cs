namespace Domain.Entities;

public class BlogPost : NArchitecture.Core.Persistence.Repositories.Entity<Guid>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid UserId { get; set; }

    public virtual User User { get; set; }
    public virtual ICollection<Comment> Comments { get; set; }

    public BlogPost()
    {
        Comments = new HashSet<Comment>();
    }

    public BlogPost(Guid id, string title, string content, Guid userId)
    {
        Id = id;
        Title = title;
        Content = content;
        UserId = userId;
    }
}
