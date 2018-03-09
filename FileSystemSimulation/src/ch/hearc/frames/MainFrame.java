package ch.hearc.frames;

import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.io.File;

import javax.swing.JFrame;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;

import ch.hearc.panels.MainPanel;

public class MainFrame extends JFrame implements ActionListener {

	private static final long serialVersionUID = -3088855809011693773L;
	public final static int FRAME_WIDTH = 1024, FRAME_HEIGHT = 800;
	
	private JMenuBar menuBar;
	private MainPanel mainPanel;
	
	public MainFrame() {
		this.initialize();
	}
	
	private void initialize() {
		this.setSize(new Dimension(FRAME_WIDTH, FRAME_HEIGHT));
		this.setPreferredSize(new Dimension(FRAME_WIDTH, FRAME_HEIGHT));
		this.setTitle("File system");
		this.setLocationRelativeTo(null);
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		this.pack();
		
		//Initialize main panel
		this.mainPanel = new MainPanel();
		this.setContentPane(mainPanel);
		
		//Initialize menu bar
		this.menuBar = new JMenuBar();
		
		//Initialize menu file
		JMenu file = new JMenu("File system");
		findFileSystem();
		this.menuBar.add(file);
		
		this.setJMenuBar(menuBar);
	}
	
	private void findFileSystem() {
		File propDir = new File("properties");
		propDir.exists();
		//D:\\HE-ARC\\ConceptionOS\\projet\\file_system_simulation\\FileSystemSimulation\\properties\\fat.properties
	}

	@Override
	public void actionPerformed(ActionEvent _e) {
		
	}
	
	public static void main(String[] args){
		MainFrame frame = new MainFrame();
		frame.setVisible(true);
	}
}
