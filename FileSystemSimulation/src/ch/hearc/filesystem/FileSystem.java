package ch.hearc.filesystem;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.util.Properties;

import ch.hearc.Utilities;
import ch.hearc.structures.Structure;

public class FileSystem {
	
	private final String NAME_PROPERTIES = "file_system_name";
	private final String NUMBER_SECTORS_PROPERTIES = "number_sectors";
	private final String SECTORS_PROPERTIES = "sectors";

	private Structure structure;
	private String propertiesPath;
	private String name;
	
	public FileSystem(String _propertiesPath) {
		this.propertiesPath = _propertiesPath;
		this.name = "";
		this.intialize();
	}
	
	private void intialize() {
		File propertiesFile = new File(propertiesPath);
		String extension = Utilities.getExtensionName(propertiesFile.getAbsolutePath());
		if(propertiesFile.exists() && propertiesFile.isFile() && extension != null && extension.equals("properties")) {
			readProperties(propertiesFile);
		}
	}
	
	private void readProperties(File _propertiesFile) {
		Properties prop = new Properties();
		try {
			prop.load(new FileInputStream(_propertiesFile));
			this.name = prop.getProperty(NAME_PROPERTIES, "no name");
			int number = Integer.parseInt(prop.getProperty(NUMBER_SECTORS_PROPERTIES, "0"));
			String[] sectors = Utilities.getPropertiesArray(prop, SECTORS_PROPERTIES, ";");
			this.structure = new Structure(number, sectors);
		} catch (IOException e) {
			e.printStackTrace();
		}
	}
	
	public Structure getStructure() {
		return structure;
	}
}
