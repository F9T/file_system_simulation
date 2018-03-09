package ch.hearc.panels;

import java.awt.Color;
import java.awt.Graphics;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.awt.event.MouseMotionListener;

import javax.swing.SwingUtilities;

import ch.hearc.filesystem.FileSystem;
import ch.hearc.structures.StructureSector;

public class StructurePanel extends DefaultPanel implements MouseListener, MouseMotionListener {

	private static final long serialVersionUID = -1478528477955937683L;
	private FileSystem fileSystem;
	private StructureSector selectedSector;
	
	public StructurePanel() {
		this.addMouseListener(this);
		this.addMouseMotionListener(this);
		this.fileSystem = null;
		this.selectedSector = null;
	}
	
	public void createFileSystem(String _pathFileSystem) {
		this.fileSystem = new FileSystem(_pathFileSystem);
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
		if(fileSystem != null) {
			fileSystem.getStructure().paint(_g);
		}
	}

	@Override
	public void mouseClicked(MouseEvent _e) {
		
	}

	@Override
	public void mouseEntered(MouseEvent _e) {
		
	}

	@Override
	public void mouseExited(MouseEvent _e) {
		if(fileSystem == null) {
			return;
		}
		if(selectedSector != null) {
			selectedSector.setBackgroundColor(Color.GRAY);
			this.repaint();
		}
	}

	@Override
	public void mousePressed(MouseEvent _e) {
		if(fileSystem == null) {
			return;
		}
		int mx = _e.getX();
		int my = _e.getY();
		if(SwingUtilities.isLeftMouseButton(_e)) {
			if(_e.getClickCount() == 2 && !_e.isConsumed()) {
				for(StructureSector sector : fileSystem.getStructure().getSectors()) {
					if(sector.intersects(mx, my)) {
						System.out.println("OUI");
					}
				}
			}
		} else if(SwingUtilities.isRightMouseButton(_e)) {
			//Context menu
		}
	}

	@Override
	public void mouseReleased(MouseEvent _e) {
	}

	@Override
	public void mouseDragged(MouseEvent _e) {
	}

	@Override
	public void mouseMoved(MouseEvent _e) {
		if(fileSystem == null) {
			return;
		}
		int mx = _e.getX();
		int my = _e.getY();
		boolean find = false;
		for(StructureSector sector : fileSystem.getStructure().getSectors()) {
			if(sector.intersects(mx, my)) {
				if(selectedSector != null) {
					selectedSector.resetColor();
					this.repaint();
				}
				selectedSector = sector;
				find = true;
				break;
			}
		}
		if(selectedSector != null && find) {
			selectedSector.setBackgroundColor(Color.GRAY);
			this.repaint();
		} else if(selectedSector != null && !find) {
			selectedSector.resetColor();
			this.repaint();
		}
	}
	
	@Override
	public void dispose() {
		this.removeMouseListener(this);
		this.removeMouseMotionListener(this);
	}
}
