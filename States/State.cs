using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GK_CAD.States
{
    public abstract class State
    {
        protected Form1 form;
        protected RelationManager relationManager;
        public State(Form1 form, RelationManager relationManager)
        {
            this.form = form;
            this.relationManager = relationManager;
        }
        public virtual void pictureBox1_MouseDown(object sender, MouseEventArgs e) { }

        public virtual void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            form.pictureBox1.Refresh();
        }

        public virtual void pictureBox1_MouseUp(object sender, MouseEventArgs e) { }
    }
}
