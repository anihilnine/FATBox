using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Windows.Forms;
using FATBox.Core;
using FATBox.Core.ModCatalog;
using FATBox.Util;
using FATBox.Util.Extensions;
using SlimDX.Direct3D11;

namespace FATBox.Ui
{
    public partial class BlueprintExplorer : Form
    {
        public BlueprintExplorer()
        {
            InitializeComponent();

            SetData(UiData.Catalog.Blueprints);
        }

        private void SetData(IEnumerable<Blueprint> blueprints)
        {
            dataNavigator.SetObject(null, blueprints.Select(x => new BlueprintGridRow(x)).ToArray());
        }


        private void cc1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            var keyword = KeywordTextbox.Text;
            var categoryExpression = new CategoryExpression(CategoryTextbox.Text);
            var filtered = UiData.Catalog.Blueprints
                .Select(blueprint => new
                {
                    Blueprint = blueprint,
                    Score = GetScore(blueprint, keyword, categoryExpression)
                })
                .Where(x => x.Score > 0)
                .OrderByDescending(x => x.Score)
                .Select(x => x.Blueprint);

            SetData(filtered);
        }

        private int GetScore(Blueprint b, string kw, CategoryExpression category)
        {
            if (!category.IsEmpty)
            {
                if (category.Matches(b)) return 11;
                return 0;
            }

            if (b.BlueprintId.FaultTolerantContains(kw)) return 10;
            if (b.General != null)
            {
                if (b.General.UnitName.FaultTolerantContains(kw)) return 5;
                if (b.Description.FaultTolerantContains(kw)) return 4;
            }
            if (b.Source.FaultTolerantContains(kw)) return 1;
            return 0;
        }
    }
}