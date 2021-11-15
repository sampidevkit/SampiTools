using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SerialPortForwarding
{
    public class RingBuffer
    {
        public struct RingTxBuf
        {
            public bool Status;
            public int Size;
            public int Head;
            public int Tail;
            public byte[] Data;
        }

        public struct RingRxBuf
        {
            public int Size;
            public int Head;
            public int Tail;
            public byte[] Data;
        }

        public bool UART_IsRxReady(RingRxBuf ringBuf)
        {
            return !(ringBuf.Head == ringBuf.Tail);
        }

        public bool UART_IsTxReady(RingTxBuf ringBuf)
        {
            int size;

            if (ringBuf.Tail < ringBuf.Head)
                size = (ringBuf.Head - ringBuf.Tail - 1);
            else
                size = (ringBuf.Size - (ringBuf.Tail - ringBuf.Head) - 1);

            return (size != 0);
        }

        public bool UART_Write(ref RingTxBuf ringBuf, byte b)
        {
            if (UART_IsTxReady(ringBuf))
            {

                ringBuf.Data[ringBuf.Tail] = b;
                ringBuf.Tail++;

                if (ringBuf.Tail >= ringBuf.Size)
                    ringBuf.Tail = 0;

                ringBuf.Status = true;

                return true;
            }

            return false;
        }

        public bool UART_Read(ref RingRxBuf ringBuf, out byte data)
        {
            if (ringBuf.Head == ringBuf.Tail)
            {
                data = ringBuf.Data[ringBuf.Head];
                ringBuf.Head++;

                if (ringBuf.Head >= ringBuf.Size)
                    ringBuf.Head = 0;

                return true;
            }

            data = 0;

            return false;
        }

        public void UART_Init(ref RingTxBuf ringTxBuf, ref RingRxBuf ringRxBuf, int size)
        {
            ringTxBuf.Head = 0;
            ringTxBuf.Tail = 0;
            ringTxBuf.Size = size;
            ringTxBuf.Status = false;
            ringTxBuf.Data = new byte[size];

            ringRxBuf.Head = 0;
            ringRxBuf.Tail = 0;
            ringRxBuf.Size = size;
            ringRxBuf.Data = new byte[size];
        }

        public void UART_Deinit(ref RingTxBuf ringTxBuf, ref RingRxBuf ringRxBuf)
        {
            ringTxBuf.Head = 0;
            ringTxBuf.Tail = 0;
            ringTxBuf.Size = 0;
            ringTxBuf.Status = false;
            ringTxBuf.Data = null;

            ringRxBuf.Head = 0;
            ringRxBuf.Tail = 0;
            ringRxBuf.Size = 0;
            ringRxBuf.Data = null;
        }

        public void UART_Tx_Isr(ref RingTxBuf RingBuf)
        {

        }

        public void UART_Rx_Isr(ref RingRxBuf RingBuf)
        {

        }
    }
}
