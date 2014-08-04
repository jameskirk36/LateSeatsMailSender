using System.IO;
using System.Net.Mail;

namespace LateSeatsMailSender
{
    public class RequestForm
    {
        private readonly string _name;
        private readonly Stream _stream;

        public RequestForm(string name, Stream stream)
        {
            _name = name;
            _stream = stream;
        }

        public Attachment CreateAttachment()
        {
            return new Attachment(_stream, _name);
        }
    }
}