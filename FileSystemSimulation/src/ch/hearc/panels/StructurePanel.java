package ch.hearc.panels;

import java.awt.Graphics;

import ch.hearc.filesystem.FileSystem;

public class StructurePanel extends DefaultPanel {

	private static final long serialVersionUID = -1478528477955937683L;
	private FileSystem fileSystem;
	
	public StructurePanel() {
		//No create here after
		this.fileSystem = new FileSystem("D:\\HE-ARC\\ConceptionOS\\projet\\file_system_simulation\\FileSystemSimulation\\properties\\fat.properties");
	}
	
	public int getStructureWidth() {
		if(fileSystem == null) {
			return 300;
		}
		return fileSystem.getStructure().getWidth();
	}
	
	@Override
	protected void paintComponent(Graphics _g) {
		super.paintComponent(_g);
		fileSystem.getStructure().paint(_g);
	}
}
