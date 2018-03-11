using Nucleus.Model;
using Nucleus.Model.Loading;
using Nucleus.Rendering;
using Salamander.Display;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Nucleus.UI;
using Salamander.Selection;

namespace Salamander.BasicTools
{

    public class LoadDisplayLayer : DisplayLayer<Load>, IAutoUIHostable
    {
        #region Properties

        private static readonly ResultsCase _AllCasesDummy = new ResultsCaseDummy("All");

        private ResultsCase _Case = _AllCasesDummy;

        [AutoUIComboBox("AvailableCases", Order=1, Label = "Case")]
        public ResultsCase Case
        {
            get { return _Case; }
            set
            {
                _Case = value;
                NotifyPropertyChanged("Case");
                InvalidateAll();
                Refresh();
            }
        }

        public ResultsCaseCollection AvailableCases
        {
            get
            {
                var result = new ResultsCaseCollection();
                result.AddRange(Core.Instance.ActiveDocument.Model.LoadCases);
                result.Add(_AllCasesDummy);
                return result;
            }
        }

        private double _ScalingFactor = 1000;

        /// <summary>
        /// The factor by which load values are scaled to determine their display length
        /// </summary>
        [AutoUISlider(2, Label = "N/Unit Scale", Max = 10000, Min = 1)]
        public double ScalingFactor
        {
            get { return _ScalingFactor; }
            set
            {
                ChangeProperty(ref _ScalingFactor, value, "ScalingFactor");
                InvalidateAll();
                Refresh();
            }
        }

        #endregion

        #region Constructor

        public LoadDisplayLayer()
            :base("Loads", "Display structural loads", 4500, Resources.URIs.Load)
        {
            Visible = true;
        }

        #endregion

        public override IList<IAvatar> GenerateRepresentations(Load source)
        {
            List<IAvatar> result = new List<IAvatar>();
            if (source != null)
            {
                if (Case == null || Case.Contains(source))
                {
                    var mAv = CreateMeshAvatar();
                    mAv.Builder.AddLoad(source, 1/ScalingFactor);
                    mAv.Brush = new ColourBrush(new Colour(0.3f, 0.8f, 0.2f, 0f)); //TODO: Make customisable
                    mAv.FinalizeMesh();
                    result.Add(mAv);
                }
            }
            return result;
        }

        /// <summary>
        /// Override function which invalidates the representations of objects as necessary when a design update occurs
        /// In this case, the offset display needs to be updated whenever node geometry changes as well as
        /// the element geometry (which is done by default)
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="e"></param>
        public override void InvalidateOnUpdate(object modified, PropertyChangedEventArgs e)
        {
            if (modified is Element || modified is Node)//TODO: Add other things loads could be applied to
            {
                foreach (var load in _Avatars.Keys.ToList())
                {
                    if (load.IsAppliedTo((ModelObject)modified))
                        InvalidateRepresentation(load);
                }
            }
            base.InvalidateOnUpdate(modified, e);
        }
    }
}
