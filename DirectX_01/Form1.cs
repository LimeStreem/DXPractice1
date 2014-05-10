using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SlimDX;
using SlimDX.Direct3D11;
using SlimDX.DXGI;
using Device = SlimDX.Direct3D11.Device;

namespace DirectX_01
{
    public partial class Form1 : Form
    {

        private SwapChain swapChain;
        private Device device;
        private RenderTargetView renderTarget;
        public Form1()
        {
            InitializeComponent();
        }

        public void Render()
        {
            device.ImmediateContext.ClearRenderTargetView(renderTarget,new Color4(1,1,0,1));
            swapChain.Present(0, PresentFlags.None);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            SwapChainDescription desc=new SwapChainDescription()
            {
                BufferCount = 2,
                Flags = SwapChainFlags.AllowModeSwitch,
                IsWindowed = true,
                ModeDescription = new ModeDescription()
                {
                    Format = Format.R8G8B8A8_UNorm,
                    Height = Height,
                    Width = Width,
                    RefreshRate = new Rational(1,60),
                    Scaling = DisplayModeScaling.Centered,
                    ScanlineOrdering = DisplayModeScanlineOrdering.Progressive
                },
                OutputHandle = Handle,
                SampleDescription = new SampleDescription(1,0),
                SwapEffect = SwapEffect.Discard,
                Usage = Usage.RenderTargetOutput
            };
            Device.CreateWithSwapChain(DriverType.Hardware, DeviceCreationFlags.None,
                new FeatureLevel[1] {FeatureLevel.Level_11_0}, desc, out device, out swapChain);
            using (Texture2D tex=Texture2D.FromSwapChain<Texture2D>(swapChain,0))
            {
                renderTarget=new RenderTargetView(device,tex);
            }
        }
    }
}
