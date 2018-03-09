package ch.hearc.frames;

import java.awt.Dimension;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.WindowEvent;
import java.awt.event.WindowListener;
import java.io.File;
import java.util.ArrayList;

import javax.swing.JFrame;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;

import ch.hearc.Settings;
import ch.hearc.filesystem.FileSystem;
import ch.hearc.interfaces.IDisposable;
import ch.hearc.panels.MainPanel;

public class MainFrame extends JFrame implements ActionListener, IDisposable, WindowListener {

	private static final long serialVersionUID = -3088855809011693773L;
	
	private JMenuBar menuBar;
	private JMenu menuFile;
	
	private MainPanel mainPanel;
	private ArrayList<FileSystem> tabFileSystem;
	
	public MainFrame() {
		this.addWindowListener(this);
		this.tabFileSystem = new ArrayList<FileSystem>();
		this.initialize();
	}
	
	private void initialize() {
		this.setSize(new Dimension(Settings.FRAME_WIDTH, Settings.FRAME_HEIGHT));
		this.setPreferredSize(new Dimension(Settings.FRAME_WIDTH, Settings.FRAME_HEIGHT));
		this.setTitle("File system");
		this.setLocationRelativeTo(null);
		this.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		this.pack();
		this.validate();
		
		//Initialize main panel
		this.mainPanel = new MainPanel();
		this.setContentPane(mainPanel);
		
		//Initialize menu bar
		this.menuBar = new JMenuBar();
		
		//Initialize menu file
		this.menuFile = new JMenu("File system");
		this.refreshFileSystem();
		
		this.menuBar.add(menuFile);
		
		this.setJMenuBar(menuBar);
	}
	
	private void findFileSystem() {
		File propDir = new File("properties");
		if(propDir.exists() && propDir.isDirectory()) {
			File[] filesSystem = propDir.listFiles((_dir, _name) -> {
				return _name.lastIndexOf(".properties") > -1;
			});
			for(File fileSystem : filesSystem) {
				tabFileSystem.add(new FileSystem(fileSystem.getAbsolutePath()));
			}
		}
	}
	
	private void refreshFileSystem() {
		tabFileSystem.clear();
		findFileSystem();
		addFileSystemToMenu();
	}
	
	private void addFileSystemToMenu() {
		menuFile.removeAll();
		for(FileSystem fileSystem : tabFileSystem) {
			JMenuItem menuItem = new JMenuItem(fileSystem.getName());
			menuItem.setActionCommand("file_system;" + fileSystem.getPath());
			menuItem.addActionListener(this);
			menuFile.add(menuItem);
		}
		menuFile.addSeparator();
		JMenuItem refreshItem = new JMenuItem("Refresh");
		refreshItem.setActionCommand("refresh");
		refreshItem.addActionListener(this);
		menuFile.add(refreshItem);
	}

	@Override
	public void actionPerformed(ActionEvent _e) {
		if(_e.getActionCommand() == null) {
			return;
		}
		String[] actionCommand = _e.getActionCommand().split(";");
		String command = actionCommand[0];
		switch(command.toLowerCase()) {
			case "file_system":
				String action = actionCommand[1];
				if(action == null || action.equals("")) {
					break;
				}
				mainPanel.createFileSystem(action);
				break;
			case "refresh":
				refreshFileSystem();
				break;
		}
	}
	
	@Override
	public void dispose() {
		mainPanel.dispose();
	}

	@Override
	public void windowActivated(WindowEvent _e) {
	}

	@Override
	public void windowClosed(WindowEvent _e) {
	}

	@Override
	public void windowClosing(WindowEvent _e) {
		mainPanel.dispose();
	}

	@Override
	public void windowDeactivated(WindowEvent _e) {
	}

	@Override
	public void windowDeiconified(WindowEvent _e) {
	}

	@Override
	public void windowIconified(WindowEvent _e) {
	}

	@Override
	public void windowOpened(WindowEvent _e) {
	}
	
	public static void main(String[] args){
		MainFrame frame = new MainFrame();
		frame.setVisible(true);
	}
}
