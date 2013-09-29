package mineswreeper.view;

import mineswreeper.controller.MWGameController;
import mineswreeper.model.MWGameCellUpdateEvent;
import mineswreeper.model.MWGameModel;
import mineswreeper.model.MWGameUpdateEvent;
import java.awt.BorderLayout;
import java.awt.Dimension;
import java.awt.GridLayout;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;
import java.awt.event.InputEvent;
import java.awt.event.MouseEvent;
import java.awt.event.MouseListener;
import java.net.URL;
import java.util.ArrayList;
import javax.swing.JOptionPane;
import javax.swing.JMenu;
import javax.swing.JMenuBar;
import javax.swing.JMenuItem;
import javax.swing.ImageIcon;
import javax.swing.JFrame;
import javax.swing.JLabel;
import javax.swing.JPanel;
import javax.swing.JButton;
import javax.swing.JTextField;

/** Classe gérant l'affichage du jeu.
 * @author Camier Mickael
 *
 */
public class MWFrameView extends MWGameView implements MouseListener
{
	/** frame du jeu
	 */
	private JFrame frame;
	/** Panel ayant pour but de contenir l'ensemble des boutons de jeu
	 */
	private JPanel panel;
	/** ArrayList contenant les boutons, leur ordre correspond à celui des MWCell dans le model, leur ordre ne doit donc pas changer
	 */
	private JPanel globalPane;
	/**
	 */
	private JMenuBar menuBar;
	/**
	 */
	private JMenu menu;
	/**
	 */
	private JMenuItem configure;
	/**
	 */
	private JMenuItem quit;
	/**
	 */
	private JMenuItem restart;
	/**
	 */
	private JLabel informations;
	/**
	 */
	private ArrayList<JButton> buttons;
	/** Valeur statique d'ordre visuel, elle décrit la taille (hauteur et largeur) d'un bouton
	 */
	private static int BUTTON_SIZE = 20;
	/** Tableau contenant les path des ImageIcon, les indices doivent correspondre au valeur du type enum MWGameModel.Context
	 */
	private static URL[] icons;
	
	
	
	/** Constructeur de la vue, la frame de jeu
	 * @param controller controleur a rattacher à la vue
	 * @param width Nombre de cases en largeur
	 * @param height Nombre de cases en hauteur
	 */
	public MWFrameView(MWGameController controller, int width, int height) {
		super(controller);
		
		icons = new URL[13];
		icons[0] = getClass().getResource("/free0.gif");
		icons[1] = getClass().getResource("/free1.gif");
		icons[2] = getClass().getResource("/free2.gif");
		icons[3] = getClass().getResource("/free3.gif");
		icons[4] = getClass().getResource("/free4.gif");
		icons[5] = getClass().getResource("/free5.gif");
		icons[6] = getClass().getResource("/free6.gif");
		icons[7] = getClass().getResource("/free7.gif");
		icons[8] = getClass().getResource("/free8.gif");
		icons[9] = getClass().getResource("/unknown.gif");
		icons[10] = getClass().getResource("/mine.gif");
		icons[11] = getClass().getResource("/wrong.gif");
		icons[12] = getClass().getResource("/marked.gif");
		
		this.buttons = new ArrayList<JButton>();
		this.initButtons(width*height);
		this.buildFrame(width, height);
	}
	

	/** Initialise les boutons, cad les crée, les place dans le ArrayList, puis leur attache un écouteur dde MouseEvent
	 * @param nbCell Nombre de cases de jeu (soit largeur*hauteur)
	 */
	private void initButtons(int nbCell)
	{
		for(int i=0 ; i < nbCell ; i++) 
		{
			JButton tempButton = new JButton(new ImageIcon(MWFrameView.icons[9]));
			tempButton.setSize(MWFrameView.BUTTON_SIZE, MWFrameView.BUTTON_SIZE);
			this.buttons.add(tempButton);
			this.buttons.get(i).addMouseListener(this);
		}
	}
	
	
	/** Construire visuellement la frame
	 * @param width Nombre de case de jeu en largeur
	 * @param height Nombre de case de jeu en hauteur
	 */
	private void buildFrame(int width, int height)
	{
		int nbCell = width*height;
		this.frame = new JFrame("Démineur de Mickael Camier (Grp 3)");
		this.frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
		this.panel = new JPanel();
		this.globalPane = new JPanel(); 
		this.informations = new JLabel("Informations");
		this.informations.setText(getController().getNbCellMarked()+" / "+getController().getNbCellMarked());
		
		this.panel.setLayout(new GridLayout(height, width));
		for(int i=0 ; i < nbCell  ; i++) 
			panel.add(this.buttons.get(i));
		
		this.globalPane.setLayout(new BorderLayout());
		this.globalPane.add(this.informations, BorderLayout.PAGE_START);
		this.panel.setPreferredSize(new Dimension(MWFrameView.BUTTON_SIZE*width,MWFrameView.BUTTON_SIZE*height+2));
		this.globalPane.add(this.panel, BorderLayout.CENTER);
		
		this.frame.setContentPane(this.globalPane);
		this.frame.setResizable(false);
		this.frame.pack();
		
		/**************************/
		this.menuBar = new JMenuBar();
		this.menu = new JMenu("Game");
		this.quit = new JMenuItem("Exit");
		this.configure = new JMenuItem("Configure");
		this.restart = new JMenuItem("Restart");
		this.menu.add(this.restart);
		this.menu.add(this.configure);
		this.menuBar.add(this.menu);
		this.menuBar.add(this.quit);
		this.frame.setJMenuBar(this.menuBar);
		/**************************/
		
		this.restart.addActionListener(new ActionListener(){
			public void actionPerformed(ActionEvent e) {
				getController().notifyReset();
			}				
		});
		this.configure.addActionListener(new ActionListener(){
			public void actionPerformed(ActionEvent e) {
				new MWConfigureView();
			}				
		});
		this.quit.addActionListener(new ActionListener(){
			public void actionPerformed(ActionEvent e) {
				System.exit(0);
			}				
		});
	}
	
	
	/* (non-Javadoc)
	 * @see mineswreeper.view.MWGameView#display()
	 */
	@Override
	public void display() 
	{
		this.frame.setVisible(true);
		
	}

	
	/* (non-Javadoc)
	 * @see mineswreeper.view.MWGameView#close()
	 */
	@Override
	public void close() 
	{
		this.frame.dispose();
	}
	
	
	/* (non-Javadoc)
	 * @see mineswreeper.model.MWGameListener#updateGrid(mineswreeper.model.MWGameCellUpdateEvent)
	 */
	@Override
	public void updateGrid(MWGameCellUpdateEvent event) 
	{
		this.informations.setText(getController().getNbCellMarked()+" / "+getController().getNbMines());
		this.buttons.get(event.getCell().getID()).setIcon(new ImageIcon((MWFrameView.icons[event.getCell().getContext().ordinal()])));
	}
	
	
	/* (non-Javadoc)
	 * @see mineswreeper.model.MWGameListener#gameWon(mineswreeper.model.MWGameUpdateEvent)
	 */
	@Override
	public void gameWon(MWGameUpdateEvent event) 
	{
		int n = JOptionPane.showConfirmDialog(
			    frame,
			    "Victoire! Voulez-vous refaire une partie ?",
			    "VICTOIRE !!!!",
			    JOptionPane.YES_NO_OPTION);
		if(n==0) getController().notifyReset();
		else System.exit(0);
	}
	
	
	/* (non-Javadoc)
	 * @see mineswreeper.model.MWGameListener#gameLost(mineswreeper.model.MWGameCellUpdateEvent)
	 */
	@Override
	public void gameLost(MWGameCellUpdateEvent event) 
	{
		// maj du bouton déclencheur pour le faire ressortir
		this.buttons.get(event.getCell().getID()).setIcon(new ImageIcon(MWFrameView.icons[MWGameModel.ContextCell.wrong.ordinal()]));
		int res = JOptionPane.showConfirmDialog(this.frame,"Voulez-vous tentez une nouvelle partie ?");
		if(res == 0) getController().notifyReset();
		else System.exit(0);
	}
	
	
	/* (non-Javadoc)
	 * @see mineswreeper.model.MWGameListener#gameReset(mineswreeper.model.MWGameUpdateEvent, int, int)
	 */
	@Override
	public void gameReset(MWGameUpdateEvent event, int width, int height) 
	{
		int nbCell = width*height;
		this.panel.removeAll();
		this.panel.setLayout(new GridLayout(height, width));
		this.panel.setPreferredSize(new Dimension(MWFrameView.BUTTON_SIZE*width,MWFrameView.BUTTON_SIZE*height+2));
		this.buttons.clear();
		this.informations.setText(getController().getNbCellMarked()+" / "+getController().getNbMines());
		this.initButtons(nbCell);
		for(int i=0 ; i < nbCell  ; i++) 
			panel.add(this.buttons.get(i));
		//this.frame.setContentPane(this.panel);
		this.frame.repaint();
		this.frame.pack();
	}

	
	/* (non-Javadoc)
	 * @see java.awt.event.MouseListener#mouseClicked(java.awt.event.MouseEvent)
	 */
	@Override
	public void mouseClicked(MouseEvent e) {
		int modifiers = e.getModifiers();
		if( (modifiers & InputEvent.BUTTON1_MASK) != 0) 
			getController().notifyCellClicked(this.buttons.indexOf(e.getSource()));
		else if( (modifiers & InputEvent.BUTTON2_MASK) != 0)
			getController().notifyCellHelp(this.buttons.indexOf(e.getSource()));
		else if( (modifiers & InputEvent.BUTTON3_MASK) != 0) 
			getController().notifyCellMarked(this.buttons.indexOf(e.getSource()));
	}

	/* (non-Javadoc)
	 * @see java.awt.event.MouseListener#mouseEntered(java.awt.event.MouseEvent)
	 */
	public void mouseEntered(MouseEvent arg0) {}
	/* (non-Javadoc)
	 * @see java.awt.event.MouseListener#mouseExited(java.awt.event.MouseEvent)
	 */
	public void mouseExited(MouseEvent arg0) {}
	/* (non-Javadoc)
	 * @see java.awt.event.MouseListener#mousePressed(java.awt.event.MouseEvent)
	 */
	public void mousePressed(MouseEvent arg0) {}
	/* (non-Javadoc)
	 * @see java.awt.event.MouseListener#mouseReleased(java.awt.event.MouseEvent)
	 */
	public void mouseReleased(MouseEvent arg0) {}
	
	private class MWConfigureView
	{
		JFrame frame;
		JPanel panel;
		JPanel fields;
		JTextField widthTextfield;
		JTextField heightTextfield;
		JTextField nbMinesTextfield;
		JButton validate;
		
		public MWConfigureView()
		{
			this.frame = new JFrame("Game's configuration");
			this.panel = new JPanel();
			this.fields = new JPanel();
			this.fields.setLayout(new GridLayout(3, 2));
			this.panel.setLayout(new GridLayout(2, 1));
			this.widthTextfield = new JTextField();
			this.heightTextfield = new JTextField();
			this.nbMinesTextfield = new JTextField();
			this.validate = new JButton("Ok");
			
			this.fields.add(new JLabel("Number of columns :"));
			this.fields.add(this.widthTextfield);
			this.fields.add(new JLabel("Number of lines :"));
			this.fields.add(this.heightTextfield);
			this.fields.add(new JLabel("Number of mines :"));
			this.fields.add(this.nbMinesTextfield);
			this.panel.add(this.fields);
			this.panel.add(this.validate);
			this.frame.setContentPane(this.panel);
			
			this.frame.setResizable(false);
			this.frame.setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
			this.frame.setVisible(true);
			this.frame.pack();
			
			this.validate.addActionListener(new ActionListener() {
				public void actionPerformed(ActionEvent e) {
					getController().notifyReset(Integer.parseInt(widthTextfield.getText()), Integer.parseInt(heightTextfield.getText()), Integer.parseInt(nbMinesTextfield.getText()));
					frame.dispose();
				}
				
			});
		}
	}

}
