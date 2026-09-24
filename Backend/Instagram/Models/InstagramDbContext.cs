using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Instagram.Models;

public partial class InstagramDbContext : DbContext
{
    public InstagramDbContext()
    {
    }

    public InstagramDbContext(DbContextOptions<InstagramDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Chat> Chats { get; set; }

    public virtual DbSet<Comentario> Comentarios { get; set; }

    public virtual DbSet<CompartidosExterno> CompartidosExternos { get; set; }

    public virtual DbSet<Guardado> Guardados { get; set; }

    public virtual DbSet<Historia> Historias { get; set; }

    public virtual DbSet<Like> Likes { get; set; }

    public virtual DbSet<Publicacione> Publicaciones { get; set; }

    public virtual DbSet<Seguidore> Seguidores { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\sqlexpress;Database=Instagram_ISO;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Chat>(entity =>
        {
            entity.HasKey(e => e.IdChat).HasName("PK__CHATS__A99B1179C590971F");

            entity.ToTable("CHATS");

            entity.Property(e => e.IdChat).HasColumnName("Id_Chat");
            entity.Property(e => e.FecHorMen)
                .HasColumnType("datetime")
                .HasColumnName("Fec_Hor_Men");
            entity.Property(e => e.IdPubComp).HasColumnName("Id_Pub_Comp");
            entity.Property(e => e.IdUsuEnv)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Id_Usu_Env");
            entity.Property(e => e.IdUsuRec)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Id_Usu_Rec");
            entity.Property(e => e.MenEnv)
                .IsUnicode(false)
                .HasColumnName("Men_Env");

            entity.HasOne(d => d.IdPubCompNavigation).WithMany(p => p.Chats)
                .HasForeignKey(d => d.IdPubComp)
                .HasConstraintName("FK__CHATS__Id_Pub_Co__6477ECF3");

            entity.HasOne(d => d.IdUsuEnvNavigation).WithMany(p => p.ChatIdUsuEnvNavigations)
                .HasForeignKey(d => d.IdUsuEnv)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHATS__Id_Usu_En__628FA481");

            entity.HasOne(d => d.IdUsuRecNavigation).WithMany(p => p.ChatIdUsuRecNavigations)
                .HasForeignKey(d => d.IdUsuRec)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__CHATS__Id_Usu_Re__6383C8BA");
        });

        modelBuilder.Entity<Comentario>(entity =>
        {
            entity.HasKey(e => e.IdCom).HasName("PK__COMENTAR__5EE02F1386D7609B");

            entity.ToTable("COMENTARIOS");

            entity.Property(e => e.IdCom).HasColumnName("Id_Com");
            entity.Property(e => e.DesCom)
                .IsUnicode(false)
                .HasColumnName("Des_Com");
            entity.Property(e => e.FecHorCom)
                .HasColumnType("datetime")
                .HasColumnName("Fec_Hor_Com");
            entity.Property(e => e.IdComRes).HasColumnName("Id_Com_Res");
            entity.Property(e => e.IdPubCom).HasColumnName("Id_Pub_Com");
            entity.Property(e => e.UsuCom)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Usu_Com");

            entity.HasOne(d => d.IdComResNavigation).WithMany(p => p.InverseIdComResNavigation)
                .HasForeignKey(d => d.IdComRes)
                .HasConstraintName("FK__COMENTARI__Id_Co__5812160E");

            entity.HasOne(d => d.IdPubComNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.IdPubCom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__COMENTARI__Id_Pu__571DF1D5");

            entity.HasOne(d => d.UsuComNavigation).WithMany(p => p.Comentarios)
                .HasForeignKey(d => d.UsuCom)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__COMENTARI__Usu_C__5629CD9C");
        });

        modelBuilder.Entity<CompartidosExterno>(entity =>
        {
            entity.HasKey(e => e.IdComp).HasName("PK__COMPARTI__ABD65167647641FB");

            entity.ToTable("COMPARTIDOS_EXTERNOS");

            entity.Property(e => e.IdComp).HasColumnName("Id_Comp");
            entity.Property(e => e.FecHorComp)
                .HasColumnType("datetime")
                .HasColumnName("Fec_Hor_Comp");
            entity.Property(e => e.IdPubComp).HasColumnName("Id_Pub_Comp");
            entity.Property(e => e.IdUsuComp)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Id_Usu_Comp");

            entity.HasOne(d => d.IdPubCompNavigation).WithMany(p => p.CompartidosExternos)
                .HasForeignKey(d => d.IdPubComp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__COMPARTID__Id_Pu__68487DD7");

            entity.HasOne(d => d.IdUsuCompNavigation).WithMany(p => p.CompartidosExternos)
                .HasForeignKey(d => d.IdUsuComp)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__COMPARTID__Id_Us__6754599E");
        });

        modelBuilder.Entity<Guardado>(entity =>
        {
            entity.HasKey(e => e.IdGua).HasName("PK__GUARDADO__5222D2FB5BE70174");

            entity.ToTable("GUARDADOS");

            entity.Property(e => e.IdGua).HasColumnName("Id_Gua");
            entity.Property(e => e.FecGua).HasColumnName("Fec_Gua");
            entity.Property(e => e.IdPubGua).HasColumnName("Id_Pub_Gua");
            entity.Property(e => e.IdUsuGua)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Id_Usu_Gua");

            entity.HasOne(d => d.IdPubGuaNavigation).WithMany(p => p.Guardados)
                .HasForeignKey(d => d.IdPubGua)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GUARDADOS__Id_Pu__5FB337D6");

            entity.HasOne(d => d.IdUsuGuaNavigation).WithMany(p => p.Guardados)
                .HasForeignKey(d => d.IdUsuGua)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__GUARDADOS__Id_Us__5EBF139D");
        });

        modelBuilder.Entity<Historia>(entity =>
        {
            entity.HasKey(e => e.IdHis).HasName("PK__HISTORIA__53E3735B05FA7596");

            entity.ToTable("HISTORIAS");

            entity.Property(e => e.IdHis).HasColumnName("Id_His");
            entity.Property(e => e.FecHorHisExp)
                .HasColumnType("datetime")
                .HasColumnName("Fec_Hor_His_Exp");
            entity.Property(e => e.FecHorHisSub)
                .HasColumnType("datetime")
                .HasColumnName("Fec_Hor_His_Sub");
            entity.Property(e => e.IdUsuHis)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Id_Usu_His");
            entity.Property(e => e.UrlHis)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Url_His");

            entity.HasOne(d => d.IdUsuHisNavigation).WithMany(p => p.Historia)
                .HasForeignKey(d => d.IdUsuHis)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__HISTORIAS__Id_Us__5070F446");
        });

        modelBuilder.Entity<Like>(entity =>
        {
            entity.HasKey(e => e.IdLike).HasName("PK__LIKES__0CCAFD6ABDB5026D");

            entity.ToTable("LIKES");

            entity.Property(e => e.IdLike).HasColumnName("Id_Like");
            entity.Property(e => e.FecHorLike)
                .HasColumnType("datetime")
                .HasColumnName("Fec_Hor_Like");
            entity.Property(e => e.IdPubLike).HasColumnName("Id_Pub_Like");
            entity.Property(e => e.IdUsuLike)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Id_Usu_Like");

            entity.HasOne(d => d.IdPubLikeNavigation).WithMany(p => p.Likes)
                .HasForeignKey(d => d.IdPubLike)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIKES__Id_Pub_Li__5AEE82B9");

            entity.HasOne(d => d.IdUsuLikeNavigation).WithMany(p => p.Likes)
                .HasForeignKey(d => d.IdUsuLike)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__LIKES__Id_Usu_Li__5BE2A6F2");
        });

        modelBuilder.Entity<Publicacione>(entity =>
        {
            entity.HasKey(e => e.IdPub).HasName("PK__PUBLICAC__51E37172649C14BB");

            entity.ToTable("PUBLICACIONES");

            entity.Property(e => e.IdPub).HasColumnName("Id_Pub");
            entity.Property(e => e.DesImgPub)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Des_Img_Pub");
            entity.Property(e => e.FecHorPub)
                .HasColumnType("datetime")
                .HasColumnName("Fec_Hor_Pub");
            entity.Property(e => e.UrlImgPub)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Url_Img_Pub");
            entity.Property(e => e.UsuPubPer)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Usu_Pub_Per");

            entity.HasOne(d => d.UsuPubPerNavigation).WithMany(p => p.Publicaciones)
                .HasForeignKey(d => d.UsuPubPer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__PUBLICACI__Usu_P__534D60F1");
        });

        modelBuilder.Entity<Seguidore>(entity =>
        {
            entity.HasKey(e => e.NumSeg).HasName("PK__SEGUIDOR__D9BA272E8C8B47EB");

            entity.ToTable("SEGUIDORES");

            entity.Property(e => e.NumSeg).HasColumnName("Num_Seg");
            entity.Property(e => e.EstSeg)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Est_Seg");
            entity.Property(e => e.FecSeg).HasColumnName("Fec_Seg");
            entity.Property(e => e.IdUsuSeg)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Id_Usu_Seg");
            entity.Property(e => e.IdUsuSig)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Id_Usu_Sig");

            entity.HasOne(d => d.IdUsuSegNavigation).WithMany(p => p.SeguidoreIdUsuSegNavigations)
                .HasForeignKey(d => d.IdUsuSeg)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SEGUIDORE__Id_Us__4D94879B");

            entity.HasOne(d => d.IdUsuSigNavigation).WithMany(p => p.SeguidoreIdUsuSigNavigations)
                .HasForeignKey(d => d.IdUsuSig)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__SEGUIDORE__Id_Us__4CA06362");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsu).HasName("PK__USUARIOS__52A331EBE126FE07");

            entity.ToTable("USUARIOS");

            entity.Property(e => e.IdUsu)
                .HasMaxLength(5)
                .IsUnicode(false)
                .HasColumnName("Id_Usu");
            entity.Property(e => e.AliasUsu)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Alias_Usu");
            entity.Property(e => e.ApeUsu)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("Ape_Usu");
            entity.Property(e => e.ConUsu)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("Con_Usu");
            entity.Property(e => e.CorUsu)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("Cor_Usu");
            entity.Property(e => e.EsPriv).HasColumnName("Es_Priv");
            entity.Property(e => e.FecReg).HasColumnName("Fec_Reg");
            entity.Property(e => e.NomUsu)
                .HasMaxLength(12)
                .IsUnicode(false)
                .HasColumnName("Nom_Usu");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
