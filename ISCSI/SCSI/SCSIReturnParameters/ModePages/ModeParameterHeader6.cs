/* Copyright (C) 2012-2016 Tal Aloni <tal.aloni.il@gmail.com>. All rights reserved.
 * 
 * You can redistribute this program and/or modify it under the terms of
 * the GNU Lesser Public License as published by the Free Software Foundation,
 * either version 3 of the License, or (at your option) any later version.
 */
using System;
using System.Collections.Generic;
using System.Text;
using Utilities;

namespace SCSI
{
    public class ModeParameterHeader6
    {
        public byte ModeDataLength; // excluding this byte
        public byte MediumType;
        public bool WP;     // Write Protect, indicates that the medium is write-protected
        public bool DPOFUA; // DPO and FUA support
        public byte BlockDescriptorLength;

        public ModeParameterHeader6()
        {
            ModeDataLength = 3;
        }

        public ModeParameterHeader6(byte[] buffer, int offset)
        {
            ModeDataLength = buffer[offset + 0];
            MediumType = buffer[offset + 1];
            WP = (buffer[offset + 2] & 0x80) != 0;
            DPOFUA = (buffer[offset + 2] & 0x10) != 0;
            BlockDescriptorLength = buffer[offset + 3];
        }

        public byte[] GetBytes()
        {
            byte[] buffer = new byte[4];
            buffer[0] = ModeDataLength;
            buffer[1] = MediumType;
            if (WP)
            {
                buffer[2] |= 0x80;
            }
            if (DPOFUA)
            {
                buffer[2] |= 0x10;
            }
            buffer[3] = BlockDescriptorLength;
            return buffer;
        }

        public int Length
        {
            get
            {
                return 4;
            }
        }
    }
}
