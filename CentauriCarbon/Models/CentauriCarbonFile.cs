using System;
using System.Collections.Generic;
using System.Text;

namespace CentauriCarbon.Models;

public class CentauriCarbonFile
{
    public string Name { get; set; } = string.Empty;

    public int Type { get; set; }

    public long CreatedTime { get; set; }

    public long FileSize { get; set; }

    public double LayerHeight { get; set; }

    public long TotalLayers { get; set; }

    public double EstFilamentLength { get; set; }
}