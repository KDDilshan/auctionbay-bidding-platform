namespace Api.Models.Email
{
    public interface EmailBuilder
    {
        void BuildTo();
        void BuildSubject();
        void BuildBody();
        public Email Build();
    }
}
