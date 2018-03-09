package ch.hearc.panels;

import java.awt.Graphics;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;

import ch.hearc.filesystem.FileSystem;
import ch.hearc.structures.StructureSector;

public class StructurePanel extends DefaultPanel implements MouseListener {

	private static final long serialVersionUID = -1478528477955937683L;
	private FileSystem fileSystem;
	
	public StructurePanel() {
		this.addMouseListener(this);
		this.fileSystem = null;
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

	@Override
	public void mouseClicked(MouseEvent _e) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mouseEntered(MouseEvent _e) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mouseExited(MouseEvent _e) {
		// TODO Auto-generated method stub
		
	}

	@Override
	public void mousePressed(MouseEvent _e) {
		if(fileSystem == null) {
			return;
		}
		for(StructureSector sector : fileSystem.getStructure().getSectors()) {
			
		}
	}

	@Override
	public void mouseReleased(MouseEvent _e) {
		// TODO Auto-generated method stub
		
	}
}
