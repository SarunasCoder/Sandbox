using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WCFClientLogger
{
    public class MyMessageInspector : IClientMessageInspector
    {
        public object BeforeSendRequest(ref System.ServiceModel.Channels.Message request, IClientChannel channel)
        {
            // Make a copy of the SOAP packet for viewing.
            MessageBuffer buffer = request.CreateBufferedCopy(Int32.MaxValue);
            Message msgCopy = buffer.CreateMessage();

            request = buffer.CreateMessage();

            // Get the SOAP XML content.
            string strMessage = buffer.CreateMessage().ToString();

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(strMessage);
            xdoc.Save("SOAP_request.xml");


            Globals.Path1 = @"...\SOAP_request.xml";

            return null;
        }

        public void AfterReceiveReply(ref System.ServiceModel.Channels.Message reply, object correlationState)
        {
            // Make a copy of the SOAP packet for viewing.
            MessageBuffer buffer = reply.CreateBufferedCopy(Int32.MaxValue);
            Message msgCopy = buffer.CreateMessage();

            reply = buffer.CreateMessage();

            // Get the SOAP XML content.
            string strMessage = buffer.CreateMessage().ToString();

            // Get the SOAP XML body content.
            System.Xml.XmlDictionaryReader xrdr = msgCopy.GetReaderAtBodyContents();
            string bodyData = xrdr.ReadOuterXml();

            // Replace the body placeholder with the actual SOAP body.
            strMessage = strMessage.Replace("... stream ...", bodyData);

            XmlDocument xdoc = new XmlDocument();
            xdoc.LoadXml(strMessage);
            xdoc.Save("SOAP_response.xml");

            Globals.Path2 = @"...\SOAP_response.xml";
        }
    }
}
