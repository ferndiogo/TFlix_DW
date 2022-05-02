using TFlix.Models;

namespace TFlix
{
    public class Admin
    {
        public Admin()
        {
            GereTable = new HashSet<Gere>();
        }


        public int Id { get; set; }

        public string Nome { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public ICollection<Gere> GereTable { get; set; }
    }
}
