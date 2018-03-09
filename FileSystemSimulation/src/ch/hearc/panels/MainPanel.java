package ch.hearc.panels;

import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.GridLayout;

import javax.swing.JSplitPane;

public class MainPanel extends DefaultPanel {

	private static final long serialVersionUID = -7957547191050945395L;
	
	private JSplitPane splitPanelFirst, splitPanelSecond;
	private StructurePanel structurePanel;

	public MainPanel() {
		this.structurePanel = new StructurePanel();
		this.splitPanelSecond = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT, structurePanel, new DefaultPanel());
		this.splitPanelFirst = new JSplitPane(JSplitPane.HORIZONTAL_SPLIT, new DefaultPanel(), splitPanelSecond);
		this.splitPanelFirst.setResizeWeight(0.3);
		this.splitPanelFirst.setDividerSize(6);
		this.splitPanelSecond.setDividerSize(6);
		
		int middlePaneWidth = structurePanel.getStructureWidth() + splitPanelFirst.getDividerSize() + splitPanelSecond.getDividerSize();
		this.structurePanel.setPreferredSize(new Dimension(middlePaneWidth, this.getPreferredSize().height));
		this.structurePanel.setMinimumSize(new Dimension(middlePaneWidth, 0));
		
		this.setLayout(new GridLayout());
		this.add(splitPanelFirst);
		repaint();
	}
	
	@Override
	protected void paintComponent(Graphics _g) {
		super.paintComponent(_g);
	}
}
