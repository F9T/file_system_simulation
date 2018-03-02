package ch.hearc.panels;

import java.awt.Color;
import java.awt.Dimension;
import java.awt.Graphics;
import java.awt.GridLayout;

import javax.swing.JPanel;

public class MainPanel extends JPanel {

	private static final long serialVersionUID = -7957547191050945395L;
	public static Color BACKGROUND_COLOR = Color.WHITE;
	
	private StructurePanel structurePanel;

	public MainPanel() {
		this.setBackground(BACKGROUND_COLOR);
		this.structurePanel = new StructurePanel();
		this.setLayout(new GridLayout(1, 1));
		this.structurePanel.setSize(new Dimension(500, 50));
		this.add(structurePanel);
		repaint();
	}
	
	@Override
	protected void paintComponent(Graphics _g) {
		super.paintComponent(_g);
	}
}
