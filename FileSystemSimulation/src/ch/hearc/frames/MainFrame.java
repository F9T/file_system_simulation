package ch.hearc.frames;

import java.awt.Dimension;

import javax.swing.JFrame;

import ch.hearc.panels.MainPanel;

public class MainFrame extends JFrame {

	private static final long serialVersionUID = -3088855809011693773L;
	public final static int FRAME_WIDTH = 1024, FRAME_HEIGHT = 800;
	
	private MainPanel mainPanel;
	
	public MainFrame() {
		this.initialize();
	}
	
	private void initialize() {
		this.setSize(new Dimension(FRAME_WIDTH, FRAME_HEIGHT));
		this.setPreferredSize(new Dimension(FRAME_WIDTH, FRAME_HEIGHT));
		this.setTitle("File system");
		this.setLocationRelativeTo(null);
		this.pack();
		
		//Initialize main panel
		this.mainPanel = new MainPanel();
		this.setContentPane(mainPanel);
	}
	
	public static void main(String[] args){
		MainFrame frame = new MainFrame();
		frame.setVisible(true);
	}
}
